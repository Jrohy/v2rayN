
namespace v2rayN
{
    class Global
    {
        #region 常量

        /// <summary>
        /// 更新链接
        /// </summary>
        public const string UpdateUrl = @"https://github.com/2dust/v2rayN/releases";
        /// <summary>
        /// 关于链接
        /// </summary>
        public const string AboutUrl = @"https://github.com/2dust/v2rayN";
        /// <summary>
        /// Donate
        /// </summary>
        public const string DonateUrl = @"https://www.paypal.me/CaptainIronNG/18";
        /// <summary>
        ///  
        /// </summary>
        public const string GithubIssuesUrl = @"https://github.com/2dust/v2rayN/issues";
        /// <summary>
        ///  
        /// </summary>
        public const string TelegramGroupUrl = @"https://t.me/v2rayN";

        /// <summary>
        /// SpeedTestUrl
        /// </summary>
        //public const string SpeedTestUrl = @"https://github.com/2dust/v2rayN/releases/download/2.1/test.zip";
        //public const string SpeedTestUrl = @"http://speedtest.tokyo2.linode.com/100MB-tokyo2.bin";
        public const string SpeedTestUrl = @"http://speedtest-sfo2.digitalocean.com/10mb.test";


        /// <summary>
        /// 本软件配置文件名
        /// </summary>
        public const string ConfigFileName = "guiNConfig.json";

        /// <summary>
        /// v2ray配置文件名
        /// </summary>
        public const string v2rayConfigFileName = "config.json";

        /// <summary>
        /// v2ray客户端配置样例文件名
        /// </summary>
        public const string v2raySampleClient = "v2rayN.Sample.SampleClientConfig.txt";
        /// <summary>
        /// v2ray服务端配置样例文件名
        /// </summary>
        public const string v2raySampleServer = "v2rayN.Sample.SampleServerConfig.txt";
        /// <summary>
        /// v2ray配置Httprequest文件名
        /// </summary>
        public const string v2raySampleHttprequestFileName = "v2rayN.Sample.SampleHttprequest.txt";
        /// <summary>
        /// v2ray配置Httpresponse文件名
        /// </summary>
        public const string v2raySampleHttpresponseFileName = "v2rayN.Sample.SampleHttpresponse.txt";


        /// <summary>
        /// 默认加密方式
        /// </summary>
        public const string DefaultSecurity = "aes-128-gcm";

        /// <summary>
        /// 默认传输协议
        /// </summary>
        public const string DefaultNetwork = "tcp";

        /// <summary>
        /// Tcp伪装http
        /// </summary>
        public const string TcpHeaderHttp = "http";

        /// <summary>
        /// None值
        /// </summary>
        public const string None = "none";

        /// <summary>
        /// 代理 tag值
        /// </summary>
        public const string agentTag = "agentout";

        /// <summary>
        /// 直连 tag值
        /// </summary>
        public const string directTag = "direct";

        /// <summary>
        /// 阻止 tag值
        /// </summary>
        public const string blockTag = "blockout";

        /// <summary>
        /// vmess
        /// </summary>
        public const string vmessProtocol = "vmess://";
        /// <summary>
        /// shadowsocks
        /// </summary>
        public const string ssProtocol = "ss://";

        /// <summary>
        /// pac
        /// </summary>
        public const string pacFILE = "pac.txt";

        /// <summary>
        /// email
        /// </summary>
        public const string userEMail = "t@t.tt";


        #endregion

        #region 全局变量

        /// <summary>
        /// 是否需要重启服务V2ray
        /// </summary>
        public static bool reloadV2ray { get; set; }

        /// <summary>
        /// 是否开启全局代理(http)
        /// </summary>
        public static bool sysAgent { get; set; }

        /// <summary>
        /// socks端口号
        /// </summary>
        public static int socksPort { get; set; }

        /// <summary>
        /// 全局代理端口(http)
        /// </summary>
        public static int sysAgentPort { get; set; }

        /// <summary>
        /// PAC监听端口号
        /// </summary>
        public static int pacPort { get; set; }

        #endregion



    }
}
