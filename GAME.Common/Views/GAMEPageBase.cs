using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GAME.Common.Core.Views
{
    public class GAMEPageBase : Page, IDisposable
    {

        private Boolean _disposed = false;

        public GAMEPageBase()
        {
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
                return;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
