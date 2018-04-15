using System;
using System.Collections.Generic;
using System.IO;
using v2rayN.Mode;

namespace v2rayN.Handler
{
    /// <summary>
    /// v2ray配置文件处理类
    /// </summary>
    class V2rayConfigHandler
    {
        private static string SampleClient = Global.v2raySampleClient;
        private static string SampleServer = Global.v2raySampleServer;

        #region 生成客户端配置

        /// <summary>
        /// 生成v2ray的客户端配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="fileName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int GenerateClientConfig(Config config, string fileName, out string msg)
        {
            msg = string.Empty;

            try
            {
                //检查GUI设置
                if (config == null
                    || config.index < 0
                    || config.vmess.Count <= 0
                    || config.index > config.vmess.Count - 1
                    )
                {
                    msg = "请先检查服务器设置";
                    return -1;
                }

                msg = "初始化配置";
                if (config.configType() == (int)EConfigType.Custom)
                {
                    return GenerateClientCustomConfig(config, fileName, out msg);
                }

                //取得默认配置
                string result = Utils.GetEmbedText(SampleClient);
                if (Utils.IsNullOrEmpty(result))
                {
                    msg = "取得默认配置失败";
                    return -1;
                }

                //转成Json
                V2rayConfig v2rayConfig = Utils.FromJson<V2rayConfig>(result);
                if (v2rayConfig == null)
                {
                    msg = "生成默认配置文件失败";
                    return -1;
                }

                //开始修改配置
                log(config, ref v2rayConfig);

                //本地端口
                inbound(config, ref v2rayConfig);

                //额外的传入连接配置
                inboundDetour(config, ref v2rayConfig);

                //路由
                routing(config, ref v2rayConfig);

                //outbound
                outbound(config, ref v2rayConfig);

                Utils.ToJsonFile(v2rayConfig, fileName);

                msg = string.Format("配置成功 \r\n{0}", config.getSummary());
            }
            catch
            {
                msg = "异常，生成默认配置文件失败";
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="config"></param>
        /// <param name="v2rayConfig"></param>
        /// <returns></returns>
        private static int log(Config config, ref V2rayConfig v2rayConfig)
        {
            try
            {
                //日志
                if (config.logEnabled)
                {
                    v2rayConfig.log.loglevel = config.loglevel;
                    v2rayConfig.log.access = Utils.GetPath("Vaccess.log");
                    v2rayConfig.log.error = Utils.GetPath("Verror.log");
                }
                else
                {
                    v2rayConfig.log.loglevel = "";
                    v2rayConfig.log.access = "";
                    v2rayConfig.log.error = "";
                }
            }
            catch
            {
            }
            return 0;
        }

        /// <summary>
        /// 本地端口
        /// </summary>
        /// <param name="config"></param>
        /// <param name="v2rayConfig"></param>
        /// <returns></returns>
        private static int inbound(Config config, ref V2rayConfig v2rayConfig)
        {
            try
            {
                //端口
                v2rayConfig.inbound.port = config.inbound[0].localPort;
                v2rayConfig.inbound.protocol = config.inbound[0].protocol;

                //开启udp
                v2rayConfig.inbound.settings.udp = config.inbound[0].udpEnabled;
            }
            catch
            {
            }
            return 0;
        }

        /// <summary>
        /// 额外的传入连接配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="v2rayConfig"></param>
        /// <returns></returns>
        private static int inboundDetour(Config config, ref V2rayConfig v2rayConfig)
        {
            try
            {
                //只有一个监听
                if (config.inbound.Count <= 1)
                {
                    return 0;
                }

                List<InboundDetourItem> inboundDetour = new List<InboundDetourItem>();
                v2rayConfig.inboundDetour = inboundDetour;

                //处理额外每个监听
                for (int k = 1; k < config.inbound.Count; k++)
                {
                    InboundDetourItem inbound = new InboundDetourItem();
                    inboundDetour.Add(inbound);

                    inbound.port = config.inbound[k].localPort.ToString();
                    inbound.listen = v2rayConfig.inbound.listen;
                    inbound.protocol = config.inbound[k].protocol;

                    Inboundsettings settings = new Inboundsettings();
                    inbound.settings = settings;
                    settings.auth = v2rayConfig.inbound.settings.auth;
                    settings.udp = config.inbound[k].udpEnabled;
                    settings.ip = v2rayConfig.inbound.settings.ip;
                }
            }
            catch
            {
            }
            return 0;
        }

        /// <summary>
        /// 路由
        /// </summary>
        /// <param name="config"></param>
        /// <param name="v2rayConfig"></param>
        /// <returns></returns>
        private static int routing(Config config, ref V2rayConfig v2rayConfig)
        {
            try
            {
                if (v2rayConfig.routing != null
                  && v2rayConfig.routing.settings != null
                  && v2rayConfig.routing.settings.rules != null)
                {
                    //自定义
                    //需代理
                    routingUserRule(config.useragent, Global.agentTag, ref v2rayConfig);
                    //直连
                    routingUserRule(config.userdirect, Global.directTag, ref v2rayConfig);
                    //阻止
                    routingUserRule(config.userblock, Global.blockTag, ref v2rayConfig);

                    //绕过大陆网址
                    if (config.chinasites)
                    {
                        //RulesItem rulesItem = new RulesItem();
                        //rulesItem.type = "chinasites";
                        //rulesItem.outboundTag = Global.directTag;
                        //v2rayConfig.routing.settings.rules.Add(rulesItem);
                        RulesItem rulesItem = new RulesItem();
                        rulesItem.type = "field";
                        rulesItem.outboundTag = Global.directTag;
                        rulesItem.domain = new List<string>();
                        rulesItem.domain.Add("geosite:cn");
                        v2rayConfig.routing.settings.rules.Add(rulesItem);
                    }
                    //绕过大陆ip
                    if (config.chinaip)
                    {
                        //RulesItem rulesItem = new RulesItem();
                        //rulesItem.type = "chinaip";
                        //rulesItem.outboundTag = Global.directTag;
                        //v2rayConfig.routing.settings.rules.Add(rulesItem);
                        RulesItem rulesItem = new RulesItem();
                        rulesItem.type = "field";
                        rulesItem.outboundTag = Global.directTag;
                        rulesItem.ip = new List<string>();
                        rulesItem.ip.Add("geoip:cn");
                        v2rayConfig.routing.settings.rules.Add(rulesItem);
                    }
                }

            }
            catch
            {
            }
            return 0;
        }
        private static int routingUserRule(List<string> userRule, string tag, ref V2rayConfig v2rayConfig)
        {
            try
            {
                if (userRule != null
                    && userRule.Count > 0)
                {
                    //Domain
                    RulesItem rulesDomain = new RulesItem();
                    rulesDomain.type = "field";
                    rulesDomain.outboundTag = tag;
                    rulesDomain.domain = new List<string>();

                    //IP
                    RulesItem rulesIP = new RulesItem();
                    rulesIP.type = "field";
                    rulesIP.outboundTag = tag;
                    rulesIP.ip = new List<string>();

                    for (int k = 0; k < userRule.Count; k++)
                    {
                        string url = userRule[k].Trim();
                        if (Utils.IsNullOrEmpty(url))
                        {
                            continue;
                        }
                        if (Utils.IsIP(url))
                        {
                            rulesIP.ip.Add(url);
                        }
                        else if (Utils.IsDomain(url))
                        {
                            rulesDomain.domain.Add(url);
                        }
                    }
                    if (rulesDomain.domain.Count > 0)
                    {
                        v2rayConfig.routing.settings.rules.Add(rulesDomain);
                    }
                    if (rulesIP.ip.Count > 0)
                    {
                        v2rayConfig.routing.settings.rules.Add(rulesIP);
                    }
                }
            }
            catch
            {
            }
            return 0;
        }

        /// <summary>
        /// vmess协议服务器配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="v2rayConfig"></param>
        /// <returns></returns>
        private static int outbound(Config config, ref V2rayConfig v2rayConfig)
        {
            try
            {
                if (config.configType() == (int)EConfigType.Vmess)
                {
                    VnextItem vnextItem;
                    if (v2rayConfig.outbound.settings.vnext.Count <= 0)
                    {
                        vnextItem = new VnextItem();
                        v2rayConfig.outbound.settings.vnext.Add(vnextItem);
                    }
                    else
                    {
                        vnextItem = v2rayConfig.outbound.settings.vnext[0];
                    }
                    //远程服务器地址和端口
                    vnextItem.address = config.address();
                    vnextItem.port = config.port();

                    UsersItem usersItem;
                    if (vnextItem.users.Count <= 0)
                    {
                        usersItem = new UsersItem();
                        vnextItem.users.Add(usersItem);
                    }
                    else
                    {
                        usersItem = vnextItem.users[0];
                    }
                    //远程服务器用户ID
                    usersItem.id = config.id();
                    usersItem.alterId = config.alterId();
                    usersItem.security = config.security();

                    //Mux
                    v2rayConfig.outbound.mux.enabled = config.muxEnabled;

                    //远程服务器底层传输配置
                    StreamSettings streamSettings = v2rayConfig.outbound.streamSettings;
                    boundStreamSettings(config, "out", ref streamSettings);

                    v2rayConfig.outbound.protocol = "vmess";
                    v2rayConfig.outbound.settings.servers = null;
                }
                else if (config.configType() == (int)EConfigType.Shadowsocks)
                {
                    ServersItem serversItem;
                    if (v2rayConfig.outbound.settings.servers.Count <= 0)
                    {
                        serversItem = new ServersItem();
                        v2rayConfig.outbound.settings.servers.Add(serversItem);
                    }
                    else
                    {
                        serversItem = v2rayConfig.outbound.settings.servers[0];
                    }
                    //远程服务器地址和端口
                    serversItem.address = config.address();
                    serversItem.port = config.port();
                    serversItem.password = config.id();
                    serversItem.method = config.security();

                    serversItem.ota = false;
                    serversItem.level = 1;

                    v2rayConfig.outbound.mux.enabled = false;

                    v2rayConfig.outbound.protocol = "shadowsocks";
                    v2rayConfig.outbound.settings.vnext = null;
                }
            }
            catch
            {
            }
            return 0;
        }

        /// <summary>
        /// vmess协议远程服务器底层传输配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="iobound"></param>
        /// <param name="streamSettings"></param>
        /// <returns></returns>
        private static int boundStreamSettings(Config config, string iobound, ref StreamSettings streamSettings)
        {
            try
            {
                //远程服务器底层传输配置
                streamSettings.network = config.network();
                streamSettings.security = config.streamSecurity();

                //streamSettings
                switch (config.network())
                {
                    //kcp基本配置暂时是默认值，用户能自己设置伪装类型
                    case "kcp":
                        KcpSettings kcpSettings = new KcpSettings();
                        kcpSettings.mtu = config.kcpItem.mtu;
                        kcpSettings.tti = config.kcpItem.tti;
                        if (iobound.Equals("out"))
                        {
                            kcpSettings.uplinkCapacity = config.kcpItem.uplinkCapacity;
                            kcpSettings.downlinkCapacity = config.kcpItem.downlinkCapacity;
                        }
                        else if (iobound.Equals("in"))
                        {
                            kcpSettings.uplinkCapacity = config.kcpItem.downlinkCapacity; ;
                            kcpSettings.downlinkCapacity = config.kcpItem.downlinkCapacity;
                        }
                        else
                        {
                            kcpSettings.uplinkCapacity = config.kcpItem.uplinkCapacity;
                            kcpSettings.downlinkCapacity = config.kcpItem.downlinkCapacity;
                        }

                        kcpSettings.congestion = config.kcpItem.congestion;
                        kcpSettings.readBufferSize = config.kcpItem.readBufferSize;
                        kcpSettings.writeBufferSize = config.kcpItem.writeBufferSize;
                        kcpSettings.header = new Header();
                        kcpSettings.header.type = config.headerType();
                        streamSettings.kcpSettings = kcpSettings;
                        break;
                    //ws
                    case "ws":
                        WsSettings wsSettings = new WsSettings();
                        wsSettings.connectionReuse = true;

                        string path = "";
                        string host2 = "";
                        //*ws(path+host),它们中间分号(;)隔开
                        string[] arrParameter = config.requestHost().Replace(" ", "").Split(';');
                        if (arrParameter.Length > 0)
                        {
                            path = arrParameter[0];
                            wsSettings.path = path;
                        }
                        if (arrParameter.Length > 1)
                        {
                            host2 = arrParameter[1];
                            wsSettings.headers = new Headers();
                            wsSettings.headers.Host = host2;
                        }

                        streamSettings.wsSettings = wsSettings;
                        break;
                    //h2
                    case "h2":
                        HttpSettings httpSettings = new HttpSettings();

                        httpSettings.path = config.requestHost().Replace(" ", "");

                        streamSettings.httpSettings = httpSettings;
                        break;
                    default:
                        //tcp带http伪装
                        if (config.headerType().Equals(Global.TcpHeaderHttp))
                        {
                            TcpSettings tcpSettings = new TcpSettings();
                            tcpSettings.connectionReuse = true;
                            tcpSettings.header = new Header();
                            tcpSettings.header.type = config.headerType();

                            //request填入自定义Host
                            string request = Utils.GetEmbedText(Global.v2raySampleHttprequestFileName);
                            string[] arrHost = config.requestHost().Replace(" ", "").Split(',');
                            string host = string.Join("\",\"", arrHost);
                            request = request.Replace("$requestHost$", string.Format("\"{0}\"", host));
                            //request = request.Replace("$requestHost$", string.Format("\"{0}\"", config.requestHost()));

                            string response = Utils.GetEmbedText(Global.v2raySampleHttpresponseFileName);

                            tcpSettings.header.request = Utils.FromJson<object>(request);
                            tcpSettings.header.response = Utils.FromJson<object>(response);
                            streamSettings.tcpSettings = tcpSettings;
                        }
                        break;
                }
            }
            catch
            {
            }
            return 0;
        }


        /// <summary>
        /// 生成v2ray的客户端配置文件(自定义配置)
        /// </summary>
        /// <param name="config"></param>
        /// <param name="fileName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int GenerateClientCustomConfig(Config config, string fileName, out string msg)
        {
            msg = string.Empty;

            try
            {
                //检查GUI设置
                if (config == null
                    || config.index < 0
                    || config.vmess.Count <= 0
                    || config.index > config.vmess.Count - 1
                    )
                {
                    msg = "请先检查服务器设置";
                    return -1;
                }

                string addressFileName = config.address();
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                File.Copy(addressFileName, fileName);

                msg = string.Format("配置成功 \r\n{0}", config.getSummary());
            }
            catch
            {
                msg = "异常，生成默认配置文件失败";
                return -1;
            }
            return 0;
        }

        #endregion

        #region 生成服务端端配置

        /// <summary>
        /// 生成v2ray的客户端配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="fileName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int GenerateServerConfig(Config config, string fileName, out string msg)
        {
            msg = string.Empty;

            try
            {
                //检查GUI设置
                if (config == null
                    || config.index < 0
                    || config.vmess.Count <= 0
                    || config.index > config.vmess.Count - 1
                    )
                {
                    msg = "请先检查服务器设置";
                    return -1;
                }

                msg = "初始化配置";

                //取得默认配置
                string result = Utils.GetEmbedText(SampleServer);
                if (Utils.IsNullOrEmpty(result))
                {
                    msg = "取得默认配置失败";
                    return -1;
                }

                //转成Json
                V2rayConfig v2rayConfig = Utils.FromJson<V2rayConfig>(result);
                if (v2rayConfig == null)
                {
                    msg = "生成默认配置文件失败";
                    return -1;
                }

                ////开始修改配置
                log(config, ref v2rayConfig);

                //vmess协议服务器配置
                ServerInbound(config, ref v2rayConfig);

                //传出设置
                ServerOutbound(config, ref v2rayConfig);

                Utils.ToJsonFile(v2rayConfig, fileName);

                msg = string.Format("配置成功 \r\n{0}", config.getSummary());
            }
            catch
            {
                msg = "异常，生成默认配置文件失败";
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// vmess协议服务器配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="v2rayConfig"></param>
        /// <returns></returns>
        private static int ServerInbound(Config config, ref V2rayConfig v2rayConfig)
        {
            try
            {
                UsersItem usersItem;
                if (v2rayConfig.inbound.settings.clients.Count <= 0)
                {
                    usersItem = new UsersItem();
                    v2rayConfig.inbound.settings.clients.Add(usersItem);
                }
                else
                {
                    usersItem = v2rayConfig.inbound.settings.clients[0];
                }
                //远程服务器端口
                v2rayConfig.inbound.port = config.port();

                //远程服务器用户ID
                usersItem.id = config.id();
                usersItem.alterId = config.alterId();

                //远程服务器底层传输配置
                StreamSettings streamSettings = v2rayConfig.inbound.streamSettings;
                boundStreamSettings(config, "in", ref streamSettings);
            }
            catch
            {
            }
            return 0;
        }

        /// <summary>
        /// 传出设置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="v2rayConfig"></param>
        /// <returns></returns>
        private static int ServerOutbound(Config config, ref V2rayConfig v2rayConfig)
        {
            try
            {
                if (v2rayConfig.outbound != null)
                {
                    v2rayConfig.outbound.settings = null;
                }
                if (v2rayConfig.outboundDetour != null
                    && v2rayConfig.outboundDetour.Count > 0)
                {
                    v2rayConfig.outboundDetour[0].settings = null;
                }
            }
            catch
            {
            }
            return 0;
        }
        #endregion

        #region 导入(导出)客户端/服务端配置

        /// <summary>
        /// 导入v2ray客户端配置
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static VmessItem ImportFromClientConfig(string fileName, out string msg)
        {
            msg = string.Empty;
            VmessItem vmessItem = new VmessItem();

            try
            {
                //载入配置文件 
                string result = Utils.LoadResource(fileName);
                if (Utils.IsNullOrEmpty(result))
                {
                    msg = "读取配置文件失败";
                    return null;
                }

                //转成Json
                V2rayConfig v2rayConfig = Utils.FromJson<V2rayConfig>(result);
                if (v2rayConfig == null)
                {
                    msg = "转换配置文件失败";
                    return null;
                }

                if (v2rayConfig.outbound == null
                    || Utils.IsNullOrEmpty(v2rayConfig.outbound.protocol)
                    || v2rayConfig.outbound.protocol != "vmess"
                    || v2rayConfig.outbound.settings == null
                    || v2rayConfig.outbound.settings.vnext == null
                    || v2rayConfig.outbound.settings.vnext.Count <= 0
                    || v2rayConfig.outbound.settings.vnext[0].users == null
                    || v2rayConfig.outbound.settings.vnext[0].users.Count <= 0)
                {
                    msg = "不是正确的客户端配置文件，请检查";
                    return null;
                }

                vmessItem.security = Global.DefaultSecurity;
                vmessItem.network = Global.DefaultNetwork;
                vmessItem.headerType = Global.None;
                vmessItem.address = v2rayConfig.outbound.settings.vnext[0].address;
                vmessItem.port = v2rayConfig.outbound.settings.vnext[0].port;
                vmessItem.id = v2rayConfig.outbound.settings.vnext[0].users[0].id;
                vmessItem.alterId = v2rayConfig.outbound.settings.vnext[0].users[0].alterId;
                vmessItem.remarks = string.Format("import@{0}", DateTime.Now.ToShortDateString());

                //tcp or kcp
                if (v2rayConfig.outbound.streamSettings != null
                    && v2rayConfig.outbound.streamSettings.network != null
                    && !Utils.IsNullOrEmpty(v2rayConfig.outbound.streamSettings.network))
                {
                    vmessItem.network = v2rayConfig.outbound.streamSettings.network;
                }

                //tcp伪装http
                if (v2rayConfig.outbound.streamSettings != null
                    && v2rayConfig.outbound.streamSettings.tcpSettings != null
                    && v2rayConfig.outbound.streamSettings.tcpSettings.header != null
                    && !Utils.IsNullOrEmpty(v2rayConfig.outbound.streamSettings.tcpSettings.header.type))
                {
                    if (v2rayConfig.outbound.streamSettings.tcpSettings.header.type.Equals(Global.TcpHeaderHttp))
                    {
                        vmessItem.headerType = v2rayConfig.outbound.streamSettings.tcpSettings.header.type;
                        string request = Convert.ToString(v2rayConfig.outbound.streamSettings.tcpSettings.header.request);
                        if (!Utils.IsNullOrEmpty(request))
                        {
                            V2rayTcpRequest v2rayTcpRequest = Utils.FromJson<V2rayTcpRequest>(request);
                            if (v2rayTcpRequest != null
                                && v2rayTcpRequest.headers != null
                                && v2rayTcpRequest.headers.Host != null
                                && v2rayTcpRequest.headers.Host.Count > 0)
                            {
                                vmessItem.requestHost = v2rayTcpRequest.headers.Host[0];
                            }
                        }
                    }
                }
                //kcp伪装
                if (v2rayConfig.outbound.streamSettings != null
                    && v2rayConfig.outbound.streamSettings.kcpSettings != null
                    && v2rayConfig.outbound.streamSettings.kcpSettings.header != null
                    && !Utils.IsNullOrEmpty(v2rayConfig.outbound.streamSettings.kcpSettings.header.type))
                {
                    vmessItem.headerType = v2rayConfig.outbound.streamSettings.kcpSettings.header.type;
                }

            }
            catch
            {
                msg = "异常，不是正确的客户端配置文件，请检查";
                return null;
            }

            return vmessItem;
        }

        /// <summary>
        /// 导入v2ray服务端配置
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static VmessItem ImportFromServerConfig(string fileName, out string msg)
        {
            msg = string.Empty;
            VmessItem vmessItem = new VmessItem();

            try
            {
                //载入配置文件 
                string result = Utils.LoadResource(fileName);
                if (Utils.IsNullOrEmpty(result))
                {
                    msg = "读取配置文件失败";
                    return null;
                }

                //转成Json
                V2rayConfig v2rayConfig = Utils.FromJson<V2rayConfig>(result);
                if (v2rayConfig == null)
                {
                    msg = "转换配置文件失败";
                    return null;
                }

                if (v2rayConfig.inbound == null
                    || Utils.IsNullOrEmpty(v2rayConfig.inbound.protocol)
                    || v2rayConfig.inbound.protocol != "vmess"
                    || v2rayConfig.inbound.settings == null
                    || v2rayConfig.inbound.settings.clients == null
                    || v2rayConfig.inbound.settings.clients.Count <= 0)
                {
                    msg = "不是正确的服务端配置文件，请检查";
                    return null;
                }

                vmessItem.security = Global.DefaultSecurity;
                vmessItem.network = Global.DefaultNetwork;
                vmessItem.headerType = Global.None;
                vmessItem.address = string.Empty;
                vmessItem.port = v2rayConfig.inbound.port;
                vmessItem.id = v2rayConfig.inbound.settings.clients[0].id;
                vmessItem.alterId = v2rayConfig.inbound.settings.clients[0].alterId;

                vmessItem.remarks = string.Format("import@{0}", DateTime.Now.ToShortDateString());

                //tcp or kcp
                if (v2rayConfig.inbound.streamSettings != null
                    && v2rayConfig.inbound.streamSettings.network != null
                    && !Utils.IsNullOrEmpty(v2rayConfig.inbound.streamSettings.network))
                {
                    vmessItem.network = v2rayConfig.inbound.streamSettings.network;
                }

                //tcp伪装http
                if (v2rayConfig.inbound.streamSettings != null
                    && v2rayConfig.inbound.streamSettings.tcpSettings != null
                    && v2rayConfig.inbound.streamSettings.tcpSettings.header != null
                    && !Utils.IsNullOrEmpty(v2rayConfig.inbound.streamSettings.tcpSettings.header.type))
                {
                    if (v2rayConfig.inbound.streamSettings.tcpSettings.header.type.Equals(Global.TcpHeaderHttp))
                    {
                        vmessItem.headerType = v2rayConfig.inbound.streamSettings.tcpSettings.header.type;
                        string request = Convert.ToString(v2rayConfig.inbound.streamSettings.tcpSettings.header.request);
                        if (!Utils.IsNullOrEmpty(request))
                        {
                            V2rayTcpRequest v2rayTcpRequest = Utils.FromJson<V2rayTcpRequest>(request);
                            if (v2rayTcpRequest != null
                                && v2rayTcpRequest.headers != null
                                && v2rayTcpRequest.headers.Host != null
                                && v2rayTcpRequest.headers.Host.Count > 0)
                            {
                                vmessItem.requestHost = v2rayTcpRequest.headers.Host[0];
                            }
                        }
                    }
                }
                ////kcp伪装
                //if (v2rayConfig.outbound.streamSettings != null
                //    && v2rayConfig.outbound.streamSettings.kcpSettings != null
                //    && v2rayConfig.outbound.streamSettings.kcpSettings.header != null
                //    && !Utils.IsNullOrEmpty(v2rayConfig.outbound.streamSettings.kcpSettings.header.type))
                //{
                //    cmbHeaderType.Text = v2rayConfig.outbound.streamSettings.kcpSettings.header.type;
                //}
            }
            catch
            {
                msg = "异常，不是正确的客户端配置文件，请检查";
                return null;
            }
            return vmessItem;
        }

        /// <summary>
        /// 从剪贴板导入URL
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="msg"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<VmessItem> ImportFromClipboardConfig(out string msg)
        {
            //载入配置文件 
            string result = Utils.GetClipboardData();
            return ImportFromStrConfig(out msg, result);
        }


        /// <summary>
        /// 传入字符串来生成vmess配置
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="msg"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<VmessItem> ImportFromStrConfig(out string msg, string str)
        {
            msg = string.Empty;
            List<VmessItem> vmessItems = new List<VmessItem>();
            try
            {

                if (Utils.IsNullOrEmpty(str))
                {
                    msg = "读取配置文件失败";
                    return null;
                }

                if (!str.Contains(Global.vmessProtocol) && !str.Contains(Global.ssProtocol))
                {
                    msg = "非vmess或ss协议";
                    return null;
                }

                //以vmess://和ss://为分割符,应对混合批量字符串
                string[] strArray = str.Trim().Split(new string[] {Global.vmessProtocol, Global.ssProtocol}, StringSplitOptions.RemoveEmptyEntries);

                foreach (string s in strArray)
                {
                    string temp = s.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                    temp = Utils.Base64Decode(temp);

                    VmessItem vmessItem = new VmessItem();

                    //vmess://字符串处理
                    if (temp.Contains("aid") && temp.Contains("ps"))
                    {
                        vmessItem.configType = (int)EConfigType.Vmess;

                        //转成Json
                        VmessQRCode vmessQRCode = Utils.FromJson<VmessQRCode>(temp);
                        if (vmessQRCode == null)
                        {
                            msg = "转换配置文件失败";
                            return null;
                        }
                        vmessItem.security = Global.DefaultSecurity;
                        vmessItem.network = Global.DefaultNetwork;
                        vmessItem.headerType = Global.None;

                        vmessItem.remarks = vmessQRCode.ps;
                        vmessItem.address = vmessQRCode.add;
                        vmessItem.port = Convert.ToInt32(vmessQRCode.port);
                        vmessItem.id = vmessQRCode.id;
                        vmessItem.alterId = Convert.ToInt32(vmessQRCode.aid);
                        vmessItem.network = vmessQRCode.net;
                        vmessItem.headerType = vmessQRCode.type;
                        vmessItem.requestHost = vmessQRCode.host;
                        vmessItem.streamSecurity = vmessQRCode.tls;

                        vmessItems.Add(vmessItem);
                    }
                    //ss://字符串处理
                    else
                    {
                        vmessItem.configType = (int)EConfigType.Shadowsocks;
                        int indexRemark = temp.IndexOf("#", StringComparison.Ordinal);
                        if (indexRemark > 0)
                        {
                            temp = temp.Substring(0, indexRemark);
                        }

                        string[] arr1 = temp.Split('@');
                        if (arr1.Length != 2)
                        {
                            return null;
                        }
                        string[] arr21 = arr1[0].Split(':');
                        string[] arr22 = arr1[1].Split(':');
                        if (arr21.Length != 2 || arr21.Length != 2)
                        {
                            return null;
                        }
                        vmessItem.address = arr22[0];
                        vmessItem.port = Convert.ToInt32(arr22[1]);
                        vmessItem.security = arr21[0];
                        vmessItem.id = arr21[1];

                        vmessItems.Add(vmessItem);
                    }
                  
                }
            }
            catch
            {
                msg = "异常，不是正确的配置，请检查";
                return null;
            }

            return vmessItems;
        }


        /// <summary>
        /// 导出为客户端配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="fileName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int Export2ClientConfig(Config config, string fileName, out string msg)
        {
            msg = string.Empty;
            return GenerateClientConfig(config, fileName, out msg);
        }

        /// <summary>
        /// 导出为服务端配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="fileName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int Export2ServerConfig(Config config, string fileName, out string msg)
        {
            msg = string.Empty;
            return GenerateServerConfig(config, fileName, out msg);
        }

        #endregion

    }
}
