using System;
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
        private static HttpWebServer wsLAN;
        private static string pacList = string.Empty;
        private static string pacListLAN = string.Empty;
        public static void Init(Config config)
        {
            string address = "127.0.0.1";
            pacList = GetPacList(address);
            string prefixes = string.Format("http://{0}:{1}/pac/", address, Global.pacPort);
            Utils.SaveLog("Webserver prefixes " + prefixes);
            ws = new HttpWebServer(SendResponse, prefixes);
            ws.Run();

            if (config.allowLANConn)
            {
                List<string> lstIPAddress = Utils.GetHostIPAddress();
                if (lstIPAddress.Count <= 0)
                {
                    return;
                }
                pacListLAN = GetPacList(lstIPAddress[0]);
                string prefixesLAN = string.Format("http://{0}:{1}/pac/", lstIPAddress[0], Global.pacPort);
                Utils.SaveLog("Webserver prefixes " + prefixesLAN);
                wsLAN = new HttpWebServer(SendResponseLAN, prefixesLAN);
                wsLAN.Run();
            }
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            //Utils.SaveLog("Webserver SendResponse");
            return pacList;
        }
        public static string SendResponseLAN(HttpListenerRequest request)
        {
            //Utils.SaveLog("Webserver SendResponseLAN");
            return pacListLAN;
        }

        public static void Stop()
        {
            if (ws != null)
            {
                Utils.SaveLog("Webserver Stop ws");
                ws.Stop();
            }
            if (wsLAN != null)
            {
                Utils.SaveLog("Webserver Stop wsLAN");
                wsLAN.Stop();
            }
        }

        private static string GetPacList(string address)
        {
            var port = Global.sysAgentPort;
            if (port <= 0)
            {
                return "No port";
            }
            try
            {
                List<string> lstProxy = new List<string>();
                lstProxy.Add(string.Format("PROXY {0}:{1};", address, port));
                var proxy = string.Join("", lstProxy.ToArray());

                string strPacfile = Utils.GetPath(Global.pacFILE);
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
