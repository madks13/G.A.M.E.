using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Common.Core.Tools.Downloader
{
    class Downloader : IDisposable
    {
        private WebClient _wc = null;

        private Boolean _disposed = false;

        public String ResourceId { get; set; }

        public Downloader()
        {
            _wc = new WebClient();
            _wc.DownloadFileCompleted += _wc_DownloadFileCompleted;
        }

        public AsyncCompletedEventHandler FileDownloaded;

        public void DownloadAsync(String url, String path)
        {
            if (String.IsNullOrEmpty(url))
                return;

            Uri uri = new Uri(url, UriKind.Absolute);
            if (!uri.IsFile)
                return;
            String file = Path.GetFileName(uri.LocalPath);
            String downloadFile = path + Path.DirectorySeparatorChar + file;
            _wc.DownloadFileAsync(uri, downloadFile);
        }

        void _wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (FileDownloaded != null)
            {
                FileDownloaded(this, e);
            }
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (!disposing)
            {

            }
            if (disposing && !_disposed)
            {
                _wc.Dispose();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
