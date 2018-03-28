using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using v2rayN.Handler;
using v2rayN.HttpProxyHandler;
using v2rayN.Mode;
using v2rayN.Tool;
using static System.Windows.Forms.ListView;
using static v2rayN.Forms.PerPixelAlphaForm;

namespace v2rayN.Forms
{
    public partial class MainForm : BaseForm
    {
        private V2rayHandler v2rayHandler;

        private PACListHandle pacListHandle;

        #region Window 事件

        public MainForm()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Text = Utils.GetVersion();

            Application.ApplicationExit += (sender, args) =>
            {
                Utils.ClearTempPath();
            };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigHandler.LoadConfig(ref config);
            v2rayHandler = new V2rayHandler();
            v2rayHandler.ProcessEvent += v2rayHandler_ProcessEvent;

            pacListHandle = new PACListHandle();
            pacListHandle.UpdateCompleted += (sender2, args) =>
            {
                if (args.Success)
                {
                    v2rayHandler_ProcessEvent(false, "PAC更新成功！");
                }
                else
                {
                    v2rayHandler_ProcessEvent(false, "PAC更新失败！");
                }
            };
            pacListHandle.Error += (sender2, args) =>
            {
                v2rayHandler_ProcessEvent(true, args.GetException().Message);
            };
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            InitServersView();
            RefreshServers();

            LoadV2ray();

            //自动从网络同步本地时间
            if (config.autoSyncTime)
            {
                //CDateTime.SetLocalTime();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                HideForm();
                return;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                HideForm();
            }
        }

        #endregion

        #region 显示服务器 listview 和 menu

        /// <summary>
        /// 刷新服务器
        /// </summary>
        private void RefreshServers()
        {
            RefreshServersView();
            RefreshServersMenu();
        }

        /// <summary>
        /// 初始化服务器列表
        /// </summary>
        private void InitServersView()
        {
            lvServers.Items.Clear();

            lvServers.GridLines = true;
            lvServers.FullRowSelect = true;
            lvServers.View = View.Details;
            lvServers.Scrollable = true;
            lvServers.MultiSelect = false;
            lvServers.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            lvServers.Columns.Add("", 30, HorizontalAlignment.Center);
            lvServers.Columns.Add("服务类型", 80, HorizontalAlignment.Left);
            lvServers.Columns.Add("别名", 100, HorizontalAlignment.Left);
            lvServers.Columns.Add("地址", 100, HorizontalAlignment.Left);
            lvServers.Columns.Add("端口", 60, HorizontalAlignment.Left);
            //lvServers.Columns.Add("用户ID(id)", 110, HorizontalAlignment.Left);
            //lvServers.Columns.Add("额外ID(alterId)", 110, HorizontalAlignment.Left);
            lvServers.Columns.Add("加密方式", 100, HorizontalAlignment.Left);
            //lvServers.Columns.Add("传输协议(network)", 120, HorizontalAlignment.Left);
            lvServers.Columns.Add("延迟", 50, HorizontalAlignment.Left);

        }

        /// <summary>
        /// 刷新服务器列表
        /// </summary>
        private void RefreshServersView()
        {
            lvServers.Items.Clear();
            lvServers.MultiSelect = true;

            for (int k = 0; k < config.vmess.Count; k++)
            {
                string def = string.Empty;
                if (config.index.Equals(k))
                {
                    def = "√";
                }

                VmessItem item = config.vmess[k];
                ListViewItem lvItem = new ListViewItem(new string[]
                {
                    def,
                    ((EConfigType)item.configType).ToString(),
                    item.remarks,
                    item.address,
                    item.port.ToString(),
                    //item.id,
                    //item.alterId.ToString(),
                    item.security,
                    //item.network,
                    ""
                });
                lvServers.Items.Add(lvItem);
            }
        }

        /// <summary>
        /// 刷新托盘服务器菜单
        /// </summary>
        private void RefreshServersMenu()
        {
            menuServers.DropDownItems.Clear();

            for (int k = 0; k < config.vmess.Count; k++)
            {
                VmessItem item = config.vmess[k];
                string name = item.getSummary();

                ToolStripMenuItem ts = new ToolStripMenuItem(name);
                ts.Tag = k;
                if (config.index.Equals(k))
                {
                    ts.Checked = true;
                }
                ts.Click += new EventHandler(ts_Click);
                menuServers.DropDownItems.Add(ts);
            }
        }

        private void ts_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem ts = (ToolStripItem)sender;
                int index = Convert.ToInt32(ts.Tag);
                SetDefaultServer(index);
            }
            catch
            {
            }
        }

        private void lvServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = -1;
            try
            {
                if (lvServers.SelectedIndices.Count > 0)
                {
                    index = lvServers.SelectedIndices[0];
                }
            }
            catch
            {
            }
            if (index < 0)
            {
                return;
            }
            qrCodeControl.showQRCode(index, config);
        }

        #endregion

        #region v2ray 操作

        /// <summary>
        /// 载入V2ray
        /// </summary>
        private void LoadV2ray()
        {
            if (Global.reloadV2ray)
            {
                ClearMsg();
            }
            v2rayHandler.LoadV2ray(config);
            Global.reloadV2ray = false;

            ChangeSysAgent(config.sysAgentEnabled);
        }

        /// <summary>
        /// 关闭V2ray
        /// </summary>
        private void CloseV2ray()
        {
            ConfigHandler.ToJsonFile(config);

            ChangeSysAgent(false);

            v2rayHandler.V2rayStop();
        }

        #endregion

        #region 功能按钮

        private void lvServers_DoubleClick(object sender, EventArgs e)
        {
            int index = GetLvSelectedIndex();
            if (index < 0)
            {
                return;
            }

            if (config.vmess[index].configType == (int)EConfigType.Vmess)
            {
                AddServerForm fm = new AddServerForm();
                fm.EditIndex = index;
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    //刷新
                    RefreshServers();
                    LoadV2ray();
                }
            }
            else if (config.vmess[index].configType == (int)EConfigType.Shadowsocks)
            {
                AddServer3Form fm = new AddServer3Form();
                fm.EditIndex = index;
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    //刷新
                    RefreshServers();
                    LoadV2ray();
                }
            }
            else
            {
                AddServer2Form fm2 = new AddServer2Form();
                fm2.EditIndex = index;
                if (fm2.ShowDialog() == DialogResult.OK)
                {
                    //刷新
                    RefreshServers();
                    LoadV2ray();
                }
            }

        }

        private void menuAddVmessServer_Click(object sender, EventArgs e)
        {
            AddServerForm fm = new AddServerForm();
            fm.EditIndex = -1;
            if (fm.ShowDialog() == DialogResult.OK)
            {
                //刷新
                RefreshServers();
                LoadV2ray();
            }
        }

        private void menuRemoveServer_Click(object sender, EventArgs e)
        {
            SelectedIndexCollection selectCollection = lvServers.SelectedIndices;
            if (selectCollection.Count < 0)
            {
                return;
            }
            if (UI.ShowYesNo("是否确定移除所选服务器?") == DialogResult.No)
            {
                return;
            }

            for(int i = 0; i < selectCollection.Count; i++)
            {
                if (ConfigHandler.RemoveServer(ref config, selectCollection[i] - i) < 0)
                {
                    break;
                }
            }
            //刷新
            RefreshServers();
            LoadV2ray();
        }

        private void menuCopyServer_Click(object sender, EventArgs e)
        {
            SelectedIndexCollection selectCollection = lvServers.SelectedIndices;
            if (selectCollection.Count < 0)
            {
                return;
            }
            for (int i = 0; i < selectCollection.Count; i++)
            {
                if (ConfigHandler.CopyServer(ref config, selectCollection[i]) < 0)
                {
                    break;
                }
            }
            //刷新
            RefreshServers();
        }

        private void menuSetDefaultServer_Click(object sender, EventArgs e)
        {
            int index = GetLvSelectedIndex();
            if (index < 0)
            {
                return;
            }
            SetDefaultServer(index);
        }

        private void menuPingServer_Click(object sender, EventArgs e)
        {
            bgwPing.RunWorkerAsync();
        }


        private void menuExport2ClientConfig_Click(object sender, EventArgs e)
        {
            int index = GetLvSelectedIndex();
            if (index < 0)
            {
                return;
            }
            if (config.vmess[index].configType != (int)EConfigType.Vmess)
            {
                UI.Show("非Vmess服务，此功能无效");
                return;
            }

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Config|*.json";
            fileDialog.FilterIndex = 2;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = fileDialog.FileName;
            if (Utils.IsNullOrEmpty(fileName))
            {
                return;
            }
            Config configCopy = Utils.DeepCopy<Config>(config);
            configCopy.index = index;
            string msg;
            if (V2rayConfigHandler.Export2ClientConfig(configCopy, fileName, out msg) != 0)
            {
                UI.Show(msg);
            }
            else
            {
                UI.Show(string.Format("客户端配置文件保存在:{0}", fileName));
            }
        }

        private void menuExport2ServerConfig_Click(object sender, EventArgs e)
        {
            int index = GetLvSelectedIndex();
            if (index < 0)
            {
                return;
            }
            if (config.vmess[index].configType != (int)EConfigType.Vmess)
            {
                UI.Show("非Vmess服务，此功能无效");
                return;
            }

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Config|*.json";
            fileDialog.FilterIndex = 2;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = fileDialog.FileName;
            if (Utils.IsNullOrEmpty(fileName))
            {
                return;
            }
            Config configCopy = Utils.DeepCopy<Config>(config);
            configCopy.index = index;
            string msg;
            if (V2rayConfigHandler.Export2ServerConfig(configCopy, fileName, out msg) != 0)
            {
                UI.Show(msg);
            }
            else
            {
                UI.Show(string.Format("服务端配置文件保存在:{0}", fileName));
            }
        }

        private void menuVmessExport2ClipBoard_Click(object sender, EventArgs e)
        {
            SelectedIndexCollection selectCollection = lvServers.SelectedIndices;
            if (selectCollection.Count < 0)
            {
                return;
            }
            List<string> urls = new List<string>();
            for (int i = 0; i < selectCollection.Count; i++)
            {
                string url = ConfigHandler.GetVmessQRCode(config, selectCollection[i]);
                urls.Add(url);
            }

            if (urls.Count > 0)
            {
                string result = String.Join(" ", urls.ToArray());
                Utils.SetClipboardData(result);
                UI.Show("链接已复制到剪切板");
            }
        }

        private void tsbOptionSetting_Click(object sender, EventArgs e)
        {
            OptionSettingForm fm = new OptionSettingForm();
            if (fm.ShowDialog() == DialogResult.OK)
            {
                //刷新
                RefreshServers();
                LoadV2ray();
            }
        }

        private void tsbReload_Click(object sender, EventArgs e)
        {
            Global.reloadV2ray = true;
            LoadV2ray();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {


            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 设置活动服务器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int SetDefaultServer(int index)
        {
            if (index < 0)
            {
                UI.Show("请先选择服务器");
                return -1;
            }
            if (ConfigHandler.SetDefaultServer(ref config, index) == 0)
            {
                //刷新
                RefreshServers();
                LoadV2ray();
            }
            return 0;
        }

        /// <summary>
        /// 取得ListView选中的行
        /// </summary>
        /// <returns></returns>
        private int GetLvSelectedIndex()
        {
            int index = -1;
            try
            {
                if (lvServers.SelectedIndices.Count <= 0)
                {
                    UI.Show("请先选择服务器");
                    return index;
                }

                index = lvServers.SelectedIndices[0];
                return index;
            }
            catch
            {
                return index;
            }
        }

        private void menuAddCustomServer_Click(object sender, EventArgs e)
        {
            UI.Show("注意,自定义配置：" +
                    "\r\n完全依赖您自己的配置，不能使用所有设置功能。" +
                    "\r\n在自定义配置inbound中有socks port等于设置中的port时，系统代理才可用");

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Config|*.json|所有文件|*.*";
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = fileDialog.FileName;
            if (Utils.IsNullOrEmpty(fileName))
            {
                return;
            }

            if (ConfigHandler.AddCustomServer(ref config, fileName) == 0)
            {
                //刷新
                RefreshServers();
                LoadV2ray();
                UI.Show(string.Format("成功导入自定义配置服务器"));
            }
            else
            {
                UI.Show(string.Format("导入自定义配置服务器失败"));
            }
        }

        private void menuAddShadowsocksServer_Click(object sender, EventArgs e)
        {
            HideForm();
            AddServer3Form fm = new AddServer3Form();
            fm.EditIndex = -1;
            if (fm.ShowDialog() == DialogResult.OK)
            {
                //刷新
                RefreshServers();
                LoadV2ray();
            }
            ShowForm();
        }

        #endregion


        #region 提示信息

        /// <summary>
        /// 消息委托
        /// </summary>
        /// <param name="notify"></param>
        /// <param name="msg"></param>
        void v2rayHandler_ProcessEvent(bool notify, string msg)
        {
            try
            {
                AppendText(msg);
                if (notify)
                {
                    notifyMsg(msg);
                }
            }
            catch
            {
            }
        }

        delegate void AppendTextDelegate(string text);

        void AppendText(string text)
        {
            if (this.txtMsgBox.InvokeRequired)
            {
                Invoke(new AppendTextDelegate(AppendText), new object[] { text });
            }
            else
            {
                //this.txtMsgBox.AppendText(text);
                ShowMsg(text);
            }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="msg"></param>
        private void ShowMsg(string msg)
        {
            this.txtMsgBox.AppendText(msg);
            if (!msg.EndsWith("\r\n"))
            {
                this.txtMsgBox.AppendText("\r\n");
            }
        }

        /// <summary>
        /// 清除信息
        /// </summary>
        private void ClearMsg()
        {
            this.txtMsgBox.Clear();
        }

        /// <summary>
        /// 托盘信息
        /// </summary>
        /// <param name="msg"></param>
        private void notifyMsg(string msg)
        {
            notifyMain.Text = msg;
        }

        #endregion


        #region 托盘事件

        private void notifyMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ShowForm();
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            CloseV2ray();

            this.Visible = false;
            this.Close();
            //this.Dispose();
            //System.Environment.Exit(System.Environment.ExitCode);
            Application.Exit();
        }

        void splash_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShowForm();
        }

        private void menuScreenQRCodeScan_Click(object sender, EventArgs e)
        {
            Thread.Sleep(100);
            foreach (Screen screen in Screen.AllScreens)
            {
                Point screen_size = Utils.GetScreenPhysicalSize();
                using (Bitmap fullImage = new Bitmap(screen_size.X,
                                                screen_size.Y))
                {
                    using (Graphics g = Graphics.FromImage(fullImage))
                    {
                        g.CopyFromScreen(screen.Bounds.X,
                                         screen.Bounds.Y,
                                         0, 0,
                                         fullImage.Size,
                                         CopyPixelOperation.SourceCopy);
                    }
                    for (int i = 0; i < 100; i++)
                    {
                        double stretch;
                        Rectangle cropRect = Scan.GetScanRect(fullImage.Width, fullImage.Height, i, out stretch);
                        if (cropRect.Width == 0)
                            break;

                        string url;
                        Rectangle rect;
                        if (stretch == 1 ? Scan.ScanQRCode(screen, fullImage, cropRect, out url, out rect) : Scan.ScanQRCodeStretch(screen, fullImage, cropRect, stretch, out url, out rect))
                        {
                            QRCodeSplashForm splash = new QRCodeSplashForm();

                            splash.FormClosed += splash_FormClosed;


                            splash.Location = new Point(screen.Bounds.X, screen.Bounds.Y);
                            double dpi = Screen.PrimaryScreen.Bounds.Width / (double)screen_size.X;
                            splash.TargetRect = new Rectangle(
                                (int)(rect.Left * dpi + screen.Bounds.X),
                                (int)(rect.Top * dpi + screen.Bounds.Y),
                                (int)(rect.Width * dpi),
                                (int)(rect.Height * dpi));
                            splash.Size = new Size(fullImage.Width, fullImage.Height);

                            List<VmessItem> vmessItems = V2rayConfigHandler.ImportFromStrConfig(out string msg, url);
                            if (vmessItems != null && vmessItems.Count > 0)
                            {
                                foreach (VmessItem vmessItem in vmessItems)
                                {
                                    if (ConfigHandler.AddServer(ref config, vmessItem, -1) != 0)
                                        break;

                                }
                                splash.Show();
                                //刷新
                                RefreshServers();
                                LoadV2ray();
                            }
                            else
                            {
                                splash.Show();
                                UI.Show(msg);
                            }

                            //扫到一个二维码即退出
                            break;
                        }
                    }
                }
            }
        }


        private void menuClipboardImportVmess_Click(object sender, EventArgs e)
        {
            List<VmessItem> vmessItems = V2rayConfigHandler.ImportFromClipboardConfig(out string msg);
            if (vmessItems != null && vmessItems.Count > 0)
            {
                foreach(VmessItem vmessItem in vmessItems)
                {
                    if (ConfigHandler.AddServer(ref config, vmessItem, -1) != 0)
                        break;
                }
                //刷新
                RefreshServers();
                LoadV2ray();
                ShowForm();

            }
            else
            {
                UI.Show("操作失败，请检查重试");
            }
        }

        private void HttpDownloadFile(string url, string path, bool overwrite, Action<string, long, long> callback = null)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //获取文件名
            string fileName = response.Headers["Content-Disposition"];//attachment;filename=FileName.txt
            if (string.IsNullOrEmpty(fileName))
                fileName = response.ResponseUri.Segments[response.ResponseUri.Segments.Length - 1];
            else
                fileName = fileName.Remove(0, fileName.IndexOf("filename=") + 9);
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            using (Stream responseStream = response.GetResponseStream())
            {
                long totalLength = response.ContentLength;
                //创建本地文件写入流
                using (Stream stream = new FileStream(Path.Combine(path, fileName), overwrite ? FileMode.Create : FileMode.CreateNew))
                {
                    byte[] bArr = new byte[1024];
                    int size;
                    while ((size = responseStream.Read(bArr, 0, bArr.Length)) > 0)
                    {
                        stream.Write(bArr, 0, size);
                        callback?.Invoke(fileName, totalLength, stream.Length);
                    }
                }
            }
        }

        private void menuUpdateV2Ray_Click(object sender, EventArgs e)
        {
            string downloadUrl = "https://github.com/v2ray/v2ray-core/releases/download/{0}/";
            string latestUrl = "https://github.com/v2ray/v2ray-core/releases/latest";
            string downloadFileName = "v2ray-windows-64.zip";

            //https的链接需设置
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //通过latestUrl的重定向来获取v2ray的最新版本号
            WebRequest req = WebRequest.Create(latestUrl);
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            string redirectUrl = response.ResponseUri.AbsoluteUri;
            string version = redirectUrl.Substring(redirectUrl.LastIndexOf("/") + 1);

            if (UI.ShowYesNo(string.Format("检测到最新的v2ray版本为:{0},是否下载安装?", version)) == DialogResult.Yes)
            {

                ShowMsg(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ") + "已提交下载任务，正在排队下载(为保证快速下载安装，期间请尽量不产生其他方面代理流量)");
                string latestDownloadUrl = string.Format(downloadUrl, version);


                WebClient client = new WebClient();

                client.DownloadProgressChanged += (sender1, args) =>
                {
                    ShowMsg(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ") + string.Format("当前接收到{0}，文件大小总共{1}，进度为{2}%", Utils.HumanReadableFilesize(args.BytesReceived), Utils.HumanReadableFilesize(args.TotalBytesToReceive), args.ProgressPercentage));
                };

                client.DownloadFileCompleted += (sender2, args) =>
                {
                    try
                    {
                        CloseV2ray();

                        //解压
                        using (ZipArchive archive = ZipFile.OpenRead(downloadFileName))
                        {
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                //如果是文件夹则跳过
                                if (entry.Length == 0)
                                    continue;
                                entry.ExtractToFile(Path.Combine(".", entry.Name), true);
                            }
                        }

                        Global.reloadV2ray = true;
                        LoadV2ray();

                        UI.Show("下载安装完成!");

                    }
                    catch (Exception)
                    {
                        if (UI.ShowYesNo("下载失败!!是否用默认浏览器下载，然后自行解压安装?") == DialogResult.Yes)
                        {
                            Global.reloadV2ray = true;
                            LoadV2ray();
                            System.Diagnostics.Process.Start(latestDownloadUrl + downloadFileName);
                        }
                    }
                    finally
                    {
                        //删除文件
                        File.Delete(downloadFileName);
                    }
                };

                //异步下载
                client.DownloadFileAsync(new Uri(latestDownloadUrl + downloadFileName), downloadFileName);
            }

        }

        private void menuUpdate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Global.UpdateUrl);
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Global.AboutUrl);
        }

        private void ShowForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            //this.notifyIcon1.Visible = false;
        }

        private void HideForm()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            this.notifyMain.Visible = true;
        }

        #endregion

        #region 后台测速

        private void bgwPing_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                for (int k = 0; k < config.vmess.Count; k++)
                {
                    if (config.vmess[k].configType == (int)EConfigType.Custom)
                    {
                        continue;
                    }
                    long time = Utils.Ping(config.vmess[k].address);
                    bgwPing.ReportProgress(k, string.Format("{0}ms", time));
                }
            }
            catch
            {
            }
        }

        private void bgwPing_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                int k = e.ProgressPercentage;
                string time = Convert.ToString(e.UserState);
                lvServers.Items[k].SubItems[lvServers.Items[k].SubItems.Count - 1].Text = time;

            }
            catch
            {
            }
        }

        #endregion

        #region 移动服务器

        private void menuMoveTop_Click(object sender, EventArgs e)
        {
            MoveServer(EMove.Top);
        }

        private void menuMoveUp_Click(object sender, EventArgs e)
        {
            MoveServer(EMove.Up);
        }

        private void menuMoveDown_Click(object sender, EventArgs e)
        {
            MoveServer(EMove.Down);
        }

        private void menuMoveBottom_Click(object sender, EventArgs e)
        {
            MoveServer(EMove.Bottom);
        }

        private void MoveServer(EMove eMove)
        {
            int index = GetLvSelectedIndex();
            if (index < 0)
            {
                UI.Show("请先选择服务器");
                return;
            }
            if (ConfigHandler.MoveServer(ref config, index, eMove) == 0)
            {
                //刷新
                RefreshServers();
                LoadV2ray();
            }
        }

        #endregion

        #region PAC相关


        private void menuUpdatePACList_Click(object sender, EventArgs e)
        {
            pacListHandle.UpdatePACFromGFWList(config);
        }

        private void menuCopyPACUrl_Click(object sender, EventArgs e)
        {
            Utils.SetClipboardData(HttpProxyHandle.GetPacUrl());
        }

        private void menuSysAgentEnabled_Click(object sender, EventArgs e)
        {
            bool isChecked = !config.sysAgentEnabled;
            config.sysAgentEnabled = isChecked;
            ChangeSysAgent(isChecked);
        }

        private void menuGlobal_Click(object sender, EventArgs e)
        {
            config.listenerType = 1;
            ChangePACButtonStatus(1);
        }

        private void menuPAC_Click(object sender, EventArgs e)
        {
            config.listenerType = 2;
            ChangePACButtonStatus(2);
        }

        private void menuKeep_Click(object sender, EventArgs e)
        {
            config.listenerType = 0;
            ChangePACButtonStatus(0);
        }

        private void ChangePACButtonStatus(int type)
        {
            if (HttpProxyHandle.Update(config, false))
            {
                switch (type)
                {
                    case 0:
                        menuGlobal.Checked = false;
                        menuKeep.Checked = true;
                        menuPAC.Checked = false;
                        break;
                    case 1:
                        menuGlobal.Checked = true;
                        menuKeep.Checked = false;
                        menuPAC.Checked = false;
                        break;
                    case 2:
                        menuGlobal.Checked = false;
                        menuKeep.Checked = false;
                        menuPAC.Checked = true;
                        break;
                }
            }

        }

        /// <summary>
        /// 改变系统代理
        /// </summary>
        /// <param name="isChecked"></param>
        private void ChangeSysAgent(bool isChecked)
        {
            if (isChecked)
            {
                if (HttpProxyHandle.RestartHttpAgent(config, false))
                {
                    ChangePACButtonStatus(config.listenerType);
                }
            }
            else
            {
                HttpProxyHandle.Update(config, true);
                HttpProxyHandle.CloseHttpAgent(config);
            }

            menuSysAgentEnabled.Checked =
            menuSysAgentMode.Enabled = isChecked;
        }




        #endregion

        private void cmsMain_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

    }
}
