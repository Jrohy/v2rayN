using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using v2rayN.Handler;
using v2rayN.Mode;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace v2rayN.Forms
{
    public partial class AddServer3Form : BaseForm
    {
        public int EditIndex { get; set; }

        public AddServer3Form()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void AddServer3Form_Load(object sender, EventArgs e)
        {
            if (EditIndex >= 0)
            {
                BindingServer();
            }
            else
            {
                ClearServer();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindingServer()
        {
            VmessItem vmessItem = config.vmess[EditIndex];

            txtAddress.Text = vmessItem.address;
            txtPort.Text = vmessItem.port.ToString();
            txtId.Text = vmessItem.id;
            cmbSecurity.Text = vmessItem.security;
            txtRemarks.Text = vmessItem.remarks;
        }


        /// <summary>
        /// 清除设置
        /// </summary>
        private void ClearServer()
        {
            txtAddress.Text = "";
            txtPort.Text = "";
            txtId.Text = "";
            cmbSecurity.Text = Global.DefaultSecurity;
            txtRemarks.Text = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Text;
            string port = txtPort.Text;
            string id = txtId.Text;
            string security = cmbSecurity.Text;
            string remarks = txtRemarks.Text;

            if (Utils.IsNullOrEmpty(address))
            {
                UI.Show("请填写服务器地址");
                return;
            }
            if (Utils.IsNullOrEmpty(port) || !Utils.IsNumberic(port))
            {
                UI.Show("请填写正确格式服务器端口");
                return;
            }
            if (Utils.IsNullOrEmpty(id))
            {
                UI.Show("请填写密码");
                return;
            }
            if (Utils.IsNullOrEmpty(security))
            {
                UI.Show("请选择加密方式");
                return;
            }

            VmessItem vmessItem = new VmessItem();
            vmessItem.address = address;
            vmessItem.port = Convert.ToInt32(port);
            vmessItem.id = id;
            vmessItem.security = security;
            vmessItem.remarks = remarks;

            if (ConfigHandler.AddShadowsocksServer(ref config, vmessItem, EditIndex) == 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                UI.Show("操作失败，请检查重试");
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


        #region 导入客户端/服务端配置

        /// <summary>
        /// 导入客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemImportClient_Click(object sender, EventArgs e)
        {
            MenuItemImport(1);
        }

        /// <summary>
        /// 导入服务端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemImportServer_Click(object sender, EventArgs e)
        {
            MenuItemImport(2);
        }

        private void MenuItemImport(int type)
        {
            //ClearServer();

            //OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Multiselect = false;
            //fileDialog.Filter = "Config|*.json|所有文件|*.*";
            //if (fileDialog.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}
            //string fileName = fileDialog.FileName;
            //if (Utils.IsNullOrEmpty(fileName))
            //{
            //    return;
            //}
            //string msg;
            //VmessItem vmessItem;
            //if (type.Equals(1))
            //{
            //    vmessItem = V2rayConfigHandler.ImportFromClientConfig(fileName, out msg);
            //}
            //else
            //{
            //    vmessItem = V2rayConfigHandler.ImportFromServerConfig(fileName, out msg);
            //}
            //if (vmessItem == null)
            //{
            //    UI.Show(msg);
            //    return;
            //}

            //txtAddress.Text = vmessItem.address;
            //txtPort.Text = vmessItem.port.ToString();
            //txtId.Text = vmessItem.id;
            //txtAlterId.Text = vmessItem.alterId.ToString();
            //txtRemarks.Text = vmessItem.remarks;
            //cmbNetwork.Text = vmessItem.network;
            //cmbHeaderType.Text = vmessItem.headerType;
            //txtRequestHost.Text = vmessItem.requestHost;
            //cmbStreamSecurity.Text = vmessItem.streamSecurity;
        }

        /// <summary>
        /// 从剪贴板导入URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemImportClipboard_Click(object sender, EventArgs e)
        {
            ImportConfig();
        }

        private void ImportConfig()
        {
            ClearServer();

            string msg;
            List<VmessItem> vmessItems = V2rayConfigHandler.ImportFromClipboardConfig(out msg);
            if (vmessItems == null || vmessItems.Count == 0)
            {
                UI.Show(msg);
                return;
            }

            txtAddress.Text = vmessItems[0].address;
            txtPort.Text = vmessItems[0].port.ToString();
            cmbSecurity.Text = vmessItems[0].security;
            txtId.Text = vmessItems[0].id;
            txtRemarks.Text = vmessItems[0].remarks;
        }

        private void menuItemScanScreen_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            bgwScan.RunWorkerAsync();
        }

        #endregion

        private void bgwScan_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string ret = scanScreen();
            bgwScan.ReportProgress(0, ret);
        }

        private void bgwScan_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;

            string result = Convert.ToString(e.UserState);
            if (string.IsNullOrEmpty(result))
            {
                UI.Show("扫描完成,未发现有效二维码");
            }
            else
            {
                Utils.SetClipboardData(result);
                ImportConfig();
            }

        }

        private string scanScreen()
        {
            string ret = string.Empty;
            try
            {
                foreach (Screen screen in Screen.AllScreens)
                {
                    using (Bitmap fullImage = new Bitmap(screen.Bounds.Width,
                                                    screen.Bounds.Height))
                    {
                        using (Graphics g = Graphics.FromImage(fullImage))
                        {
                            g.CopyFromScreen(screen.Bounds.X,
                                             screen.Bounds.Y,
                                             0, 0,
                                             fullImage.Size,
                                             CopyPixelOperation.SourceCopy);
                        }
                        int maxTry = 10;
                        for (int i = 0; i < maxTry; i++)
                        {
                            int marginLeft = (int)((double)fullImage.Width * i / 2.5 / maxTry);
                            int marginTop = (int)((double)fullImage.Height * i / 2.5 / maxTry);
                            Rectangle cropRect = new Rectangle(marginLeft, marginTop, fullImage.Width - marginLeft * 2, fullImage.Height - marginTop * 2);
                            Bitmap target = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);

                            double imageScale = (double)screen.Bounds.Width / (double)cropRect.Width;
                            using (Graphics g = Graphics.FromImage(target))
                            {
                                g.DrawImage(fullImage, new Rectangle(0, 0, target.Width, target.Height),
                                                cropRect,
                                                GraphicsUnit.Pixel);
                            }

                            var source = new BitmapLuminanceSource(target);
                            var bitmap = new BinaryBitmap(new HybridBinarizer(source));
                            QRCodeReader reader = new QRCodeReader();
                            var result = reader.decode(bitmap);
                            if (result != null)
                            {
                                ret = result.Text;
                                return ret;
                            }
                        }
                    }
                }
            }
            catch { }
            return string.Empty;
        }
    }
}
