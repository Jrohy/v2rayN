namespace v2rayN.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            this.notifyMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSysAgentEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSysAgentMode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGlobal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPAC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuKeep = new System.Windows.Forms.ToolStripMenuItem();
            this.menuServers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyPACUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.bgwPing = new System.ComponentModel.BackgroundWorker();
            this.cmsLv = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuAddVmessServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddShadowsocksServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddCustomServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddServers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuRemoveServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSetDefaultServer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMoveTop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMoveBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.menuPingServer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExport2ClientConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExport2ServerConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExport2ShareUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbServer = new System.Windows.Forms.ToolStripDropDownButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMsgBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvServers = new System.Windows.Forms.ListView();
            this.qrCodeControl = new v2rayN.Forms.QRCodeControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOptionSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCheckUpdate = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbCheckUpdateN = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbCheckUpdateCore = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbCheckUpdatePACList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.cmsMain.SuspendLayout();
            this.cmsLv.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyMain
            // 
            this.notifyMain.ContextMenuStrip = this.cmsMain;
            this.notifyMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyMain.Icon")));
            this.notifyMain.Text = "v2rayN";
            this.notifyMain.Visible = true;
            this.notifyMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyMain_MouseClick);
            // 
            // cmsMain
            // 
            this.cmsMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSysAgentEnabled,
            this.menuSysAgentMode,
            this.menuServers,
            this.menuCopyPACUrl,
            this.toolStripSeparator2,
            this.menuDonate,
            this.menuAbout,
            this.toolStripSeparator8,
            this.menuExit});
            this.cmsMain.Name = "contextMenuStrip1";
            this.cmsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsMain.ShowCheckMargin = true;
            this.cmsMain.ShowImageMargin = false;
            this.cmsMain.Size = new System.Drawing.Size(161, 170);
            // 
            // menuSysAgentEnabled
            // 
            this.menuSysAgentEnabled.Name = "menuSysAgentEnabled";
            this.menuSysAgentEnabled.Size = new System.Drawing.Size(160, 22);
            this.menuSysAgentEnabled.Text = "启用系统代理";
            this.menuSysAgentEnabled.Click += new System.EventHandler(this.menuSysAgentEnabled_Click);
            // 
            // menuSysAgentMode
            // 
            this.menuSysAgentMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGlobal,
            this.menuPAC,
            this.toolStripMenuItem2,
            this.menuKeep});
            this.menuSysAgentMode.Name = "menuSysAgentMode";
            this.menuSysAgentMode.Size = new System.Drawing.Size(160, 22);
            this.menuSysAgentMode.Text = "系统代理模式";
            // 
            // menuGlobal
            // 
            this.menuGlobal.Name = "menuGlobal";
            this.menuGlobal.Size = new System.Drawing.Size(178, 22);
            this.menuGlobal.Text = "全局模式";
            this.menuGlobal.Click += new System.EventHandler(this.menuGlobal_Click);
            // 
            // menuPAC
            // 
            this.menuPAC.Name = "menuPAC";
            this.menuPAC.Size = new System.Drawing.Size(178, 22);
            this.menuPAC.Text = "PAC模式";
            this.menuPAC.Click += new System.EventHandler(this.menuPAC_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(175, 6);
            // 
            // menuKeep
            // 
            this.menuKeep.Name = "menuKeep";
            this.menuKeep.Size = new System.Drawing.Size(178, 22);
            this.menuKeep.Text = "保持当前模式不变更";
            this.menuKeep.Click += new System.EventHandler(this.menuKeep_Click);
            // 
            // menuServers
            // 
            this.menuServers.Name = "menuServers";
            this.menuServers.Size = new System.Drawing.Size(160, 22);
            this.menuServers.Text = "服务器";
            // 
            // menuCopyPACUrl
            // 
            this.menuCopyPACUrl.Name = "menuCopyPACUrl";
            this.menuCopyPACUrl.Size = new System.Drawing.Size(160, 22);
            this.menuCopyPACUrl.Text = "复制本地PAC网址";
            this.menuCopyPACUrl.Click += new System.EventHandler(this.menuCopyPACUrl_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // menuDonate
            // 
            this.menuDonate.Name = "menuDonate";
            this.menuDonate.Size = new System.Drawing.Size(160, 22);
            this.menuDonate.Text = "捐赠";
            this.menuDonate.Click += new System.EventHandler(this.menuDonate_Click);
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(160, 22);
            this.menuAbout.Text = "关于";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(157, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(160, 22);
            this.menuExit.Text = "退出";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // bgwPing
            // 
            this.bgwPing.WorkerReportsProgress = true;
            this.bgwPing.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwPing_DoWork);
            this.bgwPing.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwPing_ProgressChanged);
            // 
            // cmsLv
            // 
            this.cmsLv.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsLv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddVmessServer,
            this.menuAddShadowsocksServer,
            this.menuAddCustomServer,
            this.menuAddServers,
            this.toolStripSeparator1,
            this.menuRemoveServer,
            this.menuCopyServer,
            this.menuSetDefaultServer,
            this.toolStripSeparator3,
            this.menuMoveTop,
            this.menuMoveUp,
            this.menuMoveDown,
            this.menuMoveBottom,
            this.toolStripSeparator9,
            this.menuPingServer,
            this.toolStripSeparator6,
            this.menuExport2ClientConfig,
            this.menuExport2ServerConfig,
            this.menuExport2ShareUrl});
            this.cmsLv.Name = "cmsLv";
            this.cmsLv.OwnerItem = this.tsbServer;
            this.cmsLv.Size = new System.Drawing.Size(227, 380);
            // 
            // menuAddVmessServer
            // 
            this.menuAddVmessServer.Name = "menuAddVmessServer";
            this.menuAddVmessServer.Size = new System.Drawing.Size(226, 22);
            this.menuAddVmessServer.Text = "添加[VMess]服务器";
            this.menuAddVmessServer.Click += new System.EventHandler(this.menuAddVmessServer_Click);
            // 
            // menuAddShadowsocksServer
            // 
            this.menuAddShadowsocksServer.Name = "menuAddShadowsocksServer";
            this.menuAddShadowsocksServer.Size = new System.Drawing.Size(226, 22);
            this.menuAddShadowsocksServer.Text = "添加[Shadowsocks]服务器";
            this.menuAddShadowsocksServer.Click += new System.EventHandler(this.menuAddShadowsocksServer_Click);
            // 
            // menuAddCustomServer
            // 
            this.menuAddCustomServer.Name = "menuAddCustomServer";
            this.menuAddCustomServer.Size = new System.Drawing.Size(226, 22);
            this.menuAddCustomServer.Text = "添加自定义配置服务器";
            this.menuAddCustomServer.Click += new System.EventHandler(this.menuAddCustomServer_Click);
            // 
            // menuAddServers
            // 
            this.menuAddServers.Name = "menuAddServers";
            this.menuAddServers.Size = new System.Drawing.Size(226, 22);
            this.menuAddServers.Text = "从剪贴板导入批量URL";
            this.menuAddServers.Click += new System.EventHandler(this.menuAddServers_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(223, 6);
            // 
            // menuRemoveServer
            // 
            this.menuRemoveServer.Name = "menuRemoveServer";
            this.menuRemoveServer.Size = new System.Drawing.Size(226, 22);
            this.menuRemoveServer.Text = "移除所选服务器   (Delete)";
            this.menuRemoveServer.Click += new System.EventHandler(this.menuRemoveServer_Click);
            // 
            // menuCopyServer
            // 
            this.menuCopyServer.Name = "menuCopyServer";
            this.menuCopyServer.Size = new System.Drawing.Size(226, 22);
            this.menuCopyServer.Text = "复制所选服务器";
            this.menuCopyServer.Click += new System.EventHandler(this.menuCopyServer_Click);
            // 
            // menuSetDefaultServer
            // 
            this.menuSetDefaultServer.Name = "menuSetDefaultServer";
            this.menuSetDefaultServer.Size = new System.Drawing.Size(226, 22);
            this.menuSetDefaultServer.Text = "设为活动服务器   (Enter)";
            this.menuSetDefaultServer.Click += new System.EventHandler(this.menuSetDefaultServer_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(223, 6);
            // 
            // menuMoveTop
            // 
            this.menuMoveTop.Name = "menuMoveTop";
            this.menuMoveTop.Size = new System.Drawing.Size(226, 22);
            this.menuMoveTop.Text = "上移至顶";
            this.menuMoveTop.Click += new System.EventHandler(this.menuMoveTop_Click);
            // 
            // menuMoveUp
            // 
            this.menuMoveUp.Name = "menuMoveUp";
            this.menuMoveUp.Size = new System.Drawing.Size(226, 22);
            this.menuMoveUp.Text = "上移      (U)";
            this.menuMoveUp.Click += new System.EventHandler(this.menuMoveUp_Click);
            // 
            // menuMoveDown
            // 
            this.menuMoveDown.Name = "menuMoveDown";
            this.menuMoveDown.Size = new System.Drawing.Size(226, 22);
            this.menuMoveDown.Text = "下移      (D)";
            this.menuMoveDown.Click += new System.EventHandler(this.menuMoveDown_Click);
            // 
            // menuMoveBottom
            // 
            this.menuMoveBottom.Name = "menuMoveBottom";
            this.menuMoveBottom.Size = new System.Drawing.Size(226, 22);
            this.menuMoveBottom.Text = "下移至底";
            this.menuMoveBottom.Click += new System.EventHandler(this.menuMoveBottom_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(223, 6);
            // 
            // menuPingServer
            // 
            this.menuPingServer.Name = "menuPingServer";
            this.menuPingServer.Size = new System.Drawing.Size(226, 22);
            this.menuPingServer.Text = "测试服务器延迟";
            this.menuPingServer.Click += new System.EventHandler(this.menuPingServer_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(223, 6);
            // 
            // menuExport2ClientConfig
            // 
            this.menuExport2ClientConfig.Name = "menuExport2ClientConfig";
            this.menuExport2ClientConfig.Size = new System.Drawing.Size(226, 22);
            this.menuExport2ClientConfig.Text = "导出所选服务器为客户端配置";
            this.menuExport2ClientConfig.Click += new System.EventHandler(this.menuExport2ClientConfig_Click);
            // 
            // menuExport2ServerConfig
            // 
            this.menuExport2ServerConfig.Name = "menuExport2ServerConfig";
            this.menuExport2ServerConfig.Size = new System.Drawing.Size(226, 22);
            this.menuExport2ServerConfig.Text = "导出所选服务器为服务端配置";
            this.menuExport2ServerConfig.Click += new System.EventHandler(this.menuExport2ServerConfig_Click);
            // 
            // menuExport2ShareUrl
            // 
            this.menuExport2ShareUrl.Name = "menuExport2ShareUrl";
            this.menuExport2ShareUrl.Size = new System.Drawing.Size(226, 22);
            this.menuExport2ShareUrl.Text = "批量导出分享URL至剪贴板";
            this.menuExport2ShareUrl.Click += new System.EventHandler(this.menuExport2ShareUrl_Click);
            // 
            // tsbServer
            // 
            this.tsbServer.DropDown = this.cmsLv;
            this.tsbServer.Image = global::v2rayN.Properties.Resources.server;
            this.tsbServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbServer.Name = "tsbServer";
            this.tsbServer.Size = new System.Drawing.Size(78, 48);
            this.tsbServer.Text = "  服务器  ";
            this.tsbServer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMsgBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 475);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(893, 176);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "信息";
            // 
            // txtMsgBox
            // 
            this.txtMsgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(49)))), ((int)(((byte)(52)))));
            this.txtMsgBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsgBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsgBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsgBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(226)))), ((int)(((byte)(228)))));
            this.txtMsgBox.Location = new System.Drawing.Point(3, 17);
            this.txtMsgBox.MaxLength = 0;
            this.txtMsgBox.Multiline = true;
            this.txtMsgBox.Name = "txtMsgBox";
            this.txtMsgBox.ReadOnly = true;
            this.txtMsgBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMsgBox.Size = new System.Drawing.Size(887, 156);
            this.txtMsgBox.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(893, 414);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器列表";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvServers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.qrCodeControl);
            this.splitContainer1.Size = new System.Drawing.Size(887, 394);
            this.splitContainer1.SplitterDistance = 600;
            this.splitContainer1.TabIndex = 2;
            // 
            // lvServers
            // 
            this.lvServers.ContextMenuStrip = this.cmsLv;
            this.lvServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvServers.FullRowSelect = true;
            this.lvServers.GridLines = true;
            this.lvServers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvServers.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvServers.Location = new System.Drawing.Point(0, 0);
            this.lvServers.Name = "lvServers";
            this.lvServers.ShowGroups = false;
            this.lvServers.Size = new System.Drawing.Size(600, 394);
            this.lvServers.TabIndex = 1;
            this.lvServers.UseCompatibleStateImageBehavior = false;
            this.lvServers.View = System.Windows.Forms.View.Details;
            this.lvServers.SelectedIndexChanged += new System.EventHandler(this.lvServers_SelectedIndexChanged);
            this.lvServers.DoubleClick += new System.EventHandler(this.lvServers_DoubleClick);
            this.lvServers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvServers_KeyDown);
            // 
            // qrCodeControl
            // 
            this.qrCodeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qrCodeControl.Location = new System.Drawing.Point(0, 0);
            this.qrCodeControl.Name = "qrCodeControl";
            this.qrCodeControl.Size = new System.Drawing.Size(283, 394);
            this.qrCodeControl.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(893, 10);
            this.panel1.TabIndex = 5;
            // 
            // tsMain
            // 
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbServer,
            this.toolStripSeparator4,
            this.tsbOptionSetting,
            this.toolStripSeparator5,
            this.tsbReload,
            this.toolStripSeparator7,
            this.tsbCheckUpdate,
            this.toolStripSeparator10,
            this.tsbClose});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(893, 51);
            this.tsMain.TabIndex = 6;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 51);
            // 
            // tsbOptionSetting
            // 
            this.tsbOptionSetting.Image = global::v2rayN.Properties.Resources.option;
            this.tsbOptionSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOptionSetting.Name = "tsbOptionSetting";
            this.tsbOptionSetting.Size = new System.Drawing.Size(81, 48);
            this.tsbOptionSetting.Text = "  参数设置  ";
            this.tsbOptionSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbOptionSetting.Click += new System.EventHandler(this.tsbOptionSetting_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 51);
            // 
            // tsbReload
            // 
            this.tsbReload.Image = ((System.Drawing.Image)(resources.GetObject("tsbReload.Image")));
            this.tsbReload.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReload.Name = "tsbReload";
            this.tsbReload.Size = new System.Drawing.Size(81, 48);
            this.tsbReload.Text = "  重启服务  ";
            this.tsbReload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbReload.Click += new System.EventHandler(this.tsbReload_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 51);
            // 
            // tsbCheckUpdate
            // 
            this.tsbCheckUpdate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCheckUpdateN,
            this.tsbCheckUpdateCore,
            this.tsbCheckUpdatePACList});
            this.tsbCheckUpdate.Image = global::v2rayN.Properties.Resources.checkupdate;
            this.tsbCheckUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheckUpdate.Name = "tsbCheckUpdate";
            this.tsbCheckUpdate.Size = new System.Drawing.Size(90, 48);
            this.tsbCheckUpdate.Text = "  检查更新  ";
            this.tsbCheckUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbCheckUpdateN
            // 
            this.tsbCheckUpdateN.Name = "tsbCheckUpdateN";
            this.tsbCheckUpdateN.Size = new System.Drawing.Size(220, 22);
            this.tsbCheckUpdateN.Text = "检查更新v2rayN";
            this.tsbCheckUpdateN.Click += new System.EventHandler(this.tsbCheckUpdateN_Click);
            // 
            // tsbCheckUpdateCore
            // 
            this.tsbCheckUpdateCore.Name = "tsbCheckUpdateCore";
            this.tsbCheckUpdateCore.Size = new System.Drawing.Size(220, 22);
            this.tsbCheckUpdateCore.Text = "检查更新v2rayCore";
            this.tsbCheckUpdateCore.Click += new System.EventHandler(this.tsbCheckUpdateCore_Click);
            // 
            // tsbCheckUpdatePACList
            // 
            this.tsbCheckUpdatePACList.Name = "tsbCheckUpdatePACList";
            this.tsbCheckUpdatePACList.Size = new System.Drawing.Size(220, 22);
            this.tsbCheckUpdatePACList.Text = "检查更新PAC(需要系统代理)";
            this.tsbCheckUpdatePACList.Click += new System.EventHandler(this.tsbCheckUpdatePACList_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 51);
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(69, 48);
            this.tsbClose.Text = "  最小化  ";
            this.tsbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 651);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tsMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "MainForm";
            this.Text = "v2rayN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.cmsMain.ResumeLayout(false);
            this.cmsLv.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMsgBox;
        private System.Windows.Forms.ListView lvServers;
        private System.Windows.Forms.NotifyIcon notifyMain;
        private System.Windows.Forms.ContextMenuStrip cmsMain;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem menuServers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.ComponentModel.BackgroundWorker bgwPing;
        private System.Windows.Forms.ContextMenuStrip cmsLv;
        private System.Windows.Forms.ToolStripMenuItem menuAddVmessServer;
        private System.Windows.Forms.ToolStripMenuItem menuRemoveServer;
        private System.Windows.Forms.ToolStripMenuItem menuSetDefaultServer;
        private System.Windows.Forms.ToolStripMenuItem menuCopyServer;
        private System.Windows.Forms.ToolStripMenuItem menuPingServer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuExport2ClientConfig;
        private System.Windows.Forms.ToolStripMenuItem menuExport2ServerConfig;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripDropDownButton tsbServer;
        private System.Windows.Forms.ToolStripButton tsbOptionSetting;
        private System.Windows.Forms.ToolStripButton tsbReload;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem menuMoveTop;
        private System.Windows.Forms.ToolStripMenuItem menuMoveUp;
        private System.Windows.Forms.ToolStripMenuItem menuMoveDown;
        private System.Windows.Forms.ToolStripMenuItem menuMoveBottom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem menuSysAgentMode;
        private System.Windows.Forms.ToolStripMenuItem menuGlobal;
        private System.Windows.Forms.ToolStripMenuItem menuPAC;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuKeep;
        private System.Windows.Forms.ToolStripMenuItem menuSysAgentEnabled;
        private System.Windows.Forms.ToolStripMenuItem menuCopyPACUrl;
        private System.Windows.Forms.ToolStripMenuItem menuAddCustomServer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuAddShadowsocksServer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private QRCodeControl qrCodeControl;
        private System.Windows.Forms.ToolStripMenuItem menuDonate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripDropDownButton tsbCheckUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsbCheckUpdateN;
        private System.Windows.Forms.ToolStripMenuItem tsbCheckUpdateCore;
        private System.Windows.Forms.ToolStripMenuItem tsbCheckUpdatePACList;
        private System.Windows.Forms.ToolStripMenuItem menuAddServers;
        private System.Windows.Forms.ToolStripMenuItem menuExport2ShareUrl;
    }
}

