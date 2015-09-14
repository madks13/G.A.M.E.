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
    public abstract class CoreModule : MarshalByRefObject, IModule
    {
        protected String _slotName = null;
        protected Page _main = null;
        protected Page _options = null;
        protected String _name = null;
        protected Window _mw = null;

        protected CoreModule(String name)
        {
            _name = name;
            //InitializeLifetimeService();
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        //public override Object InitializeLifetimeService()
        //{
        //    ILease lease = (ILease)base.InitializeLifetimeService();

        //    // Normally, the initial lease time would be much longer.
        //    // It is shortened here for demonstration purposes.
        //    if (lease.CurrentState == LeaseState.Initial)
        //    {
        //        lease.InitialLeaseTime = TimeSpan.Zero;
        //        //lease.SponsorshipTimeout = TimeSpan.FromHours(1);
        //        //lease.RenewOnCallTime = TimeSpan.FromHours(1);
        //    }
        //    return lease;
        //}

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

        public abstract Boolean ShowMain();

        public abstract Boolean ShowOptions();

        public virtual Boolean Hide()
        {
            if (_mw == null)
                return false;
            _mw.Hide();
            return true;
        }

        public virtual Boolean Stop()
        {
            if (_mw != null)
            {
                
                _mw = null;
                
                //NotifyPropertyChanged("IsLaunched");
                //NotifyPropertyChanged("IsVisible");
                return true;
            }
            return false;
        }

        //#region PropertyChangedEventHandler

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void RaisePropertyChanged(string propertyName)
        //{
        //    var handler = PropertyChanged;

        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        //protected void NotifyPropertyChanged(string propName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propName));
        //}

        //#endregion


        public event EventHandler Closed;
    }
}
