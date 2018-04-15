using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using v2rayN.Mode;
using v2rayN.Properties;
using v2rayN.Tool;

namespace v2rayN.HttpProxyHandler
{
    /// <summary>
    /// 提供PAC功能支持
    /// </summary>
    class PACServerHandle
    {
        private static HttpWebServer ws;
        private static string pacList = string.Empty;
        public static void Init(Config config)
        {
            pacList = GetPacList();

            string prefixes = string.Format("http://127.0.0.1:{0}/pac/", Global.pacPort);
            ws = new HttpWebServer(SendResponse, prefixes);
            ws.Run();
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            if (string.IsNullOrEmpty(pacList))
            {
                pacList = GetPacList();
            }

            return pacList;
        }

        public static void Stop()
        {
            if (ws != null)
            {
                ws.Stop();
            }
        }
        
        private static string GetPacList()
        {
            var port = Global.sysAgentPort;
            if (port <= 0)
            {
                return "No port";
            }
            try
            {
                List<string> lstProxy = new List<string>();
                lstProxy.Add(string.Format("PROXY 127.0.0.1:{0};", port));

                List<string> lstIPAddress = Utils.GetHostIPAddress();
                foreach (string ip in lstIPAddress)
                {
                    lstProxy.Add(string.Format("PROXY {1}:{0};", port, ip));
                }
                var proxy = string.Join("", lstProxy.ToArray());

                string strPacfile = Utils.GetPath(Global.PAC_FILE);
                if (!File.Exists(strPacfile))
                {
                    FileManager.UncompressFile(strPacfile, Resources.pac_txt);
                }
                var pac = File.ReadAllText(strPacfile, Encoding.UTF8);
                pac = pac.Replace("__PROXY__", proxy);
                return pac;
            }
            catch
            { }
            return "No pac content";
        }
    }
}
