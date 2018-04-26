using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using v2rayN.Mode;
using v2rayN.Properties;

namespace v2rayN.Handler
{
    /// <summary>
    ///Update V2ray Core 
    /// </summary>
    class V2rayUpdateHandle
    {
        public event EventHandler<ResultEventArgs> UpdateCompleted;

        public event ErrorEventHandler Error;

        public class ResultEventArgs : EventArgs
        {
            public bool Success;
            public string Msg;

            public ResultEventArgs(bool success, string msg)
            {
                this.Success = success;
                this.Msg = msg;
            }
        }

        private string latestUrl = "https://github.com/v2ray/v2ray-core/releases/latest";
        private const string coreURL = "https://github.com/v2ray/v2ray-core/releases/download/{0}/v2ray-windows-{1}.zip";
        private int progressPercentage = -1;
        private string fileName = "v2ray-windows.zip";

        public void UpdateV2rayCore(Config config)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2

            WebRequest request = WebRequest.Create(latestUrl);
            request.BeginGetResponse(new AsyncCallback(OnResponse), request);
        }

        private void OnResponse(IAsyncResult ar)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
                string redirectUrl = response.ResponseUri.AbsoluteUri;
                string version = redirectUrl.Substring(redirectUrl.LastIndexOf("/", StringComparison.Ordinal) + 1);

                string osBit = string.Empty;
                if (Environment.Is64BitProcess)
                {
                    osBit = "64";
                }
                else
                {
                    osBit = "32";
                }
                string url = string.Format(coreURL, version, osBit);
                if (UpdateCompleted != null)
                {
                    UpdateCompleted(this, new ResultEventArgs(false, url));
                }

                progressPercentage = -1;

                WebClient ws = new WebClient();
                ws.DownloadFileCompleted += ws_DownloadFileCompleted;
                ws.DownloadProgressChanged += ws_DownloadProgressChanged;
                ws.DownloadFileAsync(new Uri(url), fileName);
            }
            catch (Exception ex)
            {
                Utils.SaveLog(ex.Message, ex);

                if (Error != null)
                    Error(this, new ErrorEventArgs(ex));
            }
        }

        void ws_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (UpdateCompleted != null)
            {
                if (progressPercentage != e.ProgressPercentage && e.ProgressPercentage % 5 == 0)
                {
                    progressPercentage = e.ProgressPercentage;
                    string msg = string.Format("......{0}%", e.ProgressPercentage);
                    UpdateCompleted(this, new ResultEventArgs(false, msg));
                }
            }
        }

        void ws_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                if (UpdateCompleted != null)
                {
                    UpdateCompleted(this, new ResultEventArgs(true, fileName));
                }
            }
            catch (Exception ex)
            {
                Utils.SaveLog(ex.Message, ex);

                if (Error != null)
                    Error(this, new ErrorEventArgs(ex));
            }
        }
    }
}
