using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Reflection;
using System.Windows.Controls;
using GAME.Common.Core.Interfaces;
using GAME.Common.Core.Interfaces.Plugin;
using System.Windows.Forms.Integration;
using System.Windows;
using System.Runtime.Remoting.Lifetime;

namespace GAME.Common.Plugin
{
    public abstract class CoreModule : MarshalByRefObject, IModule, IDisposable
    {
        //protected String _slotName = null;
        //protected Page _main = null;
        //protected Page _options = null;
        protected String _name = null;
        protected Window _mw = null;
        private Boolean _disposed;

        protected CoreModule(String name)
        {
            _name = name;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public Window MainWindow
        {
            get { return _mw; }
            set { _mw = value; }
        }

        public Boolean IsLaunched 
        {
            get { return _mw != null; } 
        }

        public abstract Boolean IsShowingMain
        {
            get;
        }

        public abstract Boolean IsShowingOptions
        {
            get ;
        }

        public Boolean IsVisible
        {
            get { return (_mw != null && _mw.IsVisible); }
        }

        public virtual string Name 
        {
            get { return _name; }
        }

        public abstract void ShowMain();

        public abstract void ShowOptions();

        public virtual void Hide()
        {
            if (_mw != null)
            {
                if (_mw.IsVisible == false)
                    _mw.Show();
                else
                    _mw.Hide();
            }
        }

        public virtual void Stop()
        {
            if (_mw != null)
            {
                _mw.Close();
                _mw = null;
            }
        }

        public event EventHandler Closed;

        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                Stop();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
