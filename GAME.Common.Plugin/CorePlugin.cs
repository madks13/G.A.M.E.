using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;

using GAME.Common.Core.Interfaces;
using GAME.Common.Core.Interfaces.Plugin;
using System.Runtime.Remoting.Lifetime;

namespace GAME.Common.Plugin
{
    public abstract class CorePlugin: MarshalByRefObject, IPlugin
    {
        protected Type _moduleType = null;

        protected IModule _moduleInstance = null;

        public abstract String Type {get;}

        public abstract string SubType {get;}

        public abstract String Name { get; }

        public Tmod GetInstance<Tmod>() where Tmod : class
        {
            if (typeof(Tmod).IsAssignableFrom(_moduleType)) 
            {
                if (_moduleInstance == null)
                {
                    Tmod mod = Activator.CreateInstance(_moduleType) as Tmod;
                    _moduleInstance = mod as IModule;
                    //var lease = ((MarshalByRefObject)_moduleInstance).InitializeLifetimeService() as ILease;
                    //lease.Register(new Sponsor());
                    //_moduleInstance.PropertyChanged += OnLaunchedStatusChanged;
                    //NotifyPropertyChanged("IsInstantiated");
                    //NotifyPropertyChanged("IsLaunched");
                }
                return _moduleInstance as Tmod;
            }
            return null;
        }

        public CorePlugin GetInstance()
        {
            if (typeof(CorePlugin).IsAssignableFrom(_moduleType))
            {
                if (_moduleInstance == null)
                {
                    CoreModule mod = Activator.CreateInstance(_moduleType) as CoreModule;
                    _moduleInstance = mod as IModule;
                    //var lease = ((MarshalByRefObject)_moduleInstance).InitializeLifetimeService() as ILease;
                    //lease.Register(new Sponsor());
                    //_moduleInstance.PropertyChanged += OnLaunchedStatusChanged;
                    //NotifyPropertyChanged("IsInstantiated");
                    //NotifyPropertyChanged("IsLaunched");
                }
                return _moduleInstance as CorePlugin;
            }
            return null;
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
            get { return (_moduleInstance != null && _moduleInstance.IsLaunched); }
        }

        public Boolean IsInstantiated
        {
            get { return _moduleInstance != null; }
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

        //private void OnLaunchedStatusChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    NotifyPropertyChanged("IsLaunched");
        //}

        //#endregion

        public void UnloadModule()
        {
            if (_moduleInstance != null)
            {
                _moduleInstance.Stop();
                _moduleInstance = null;
            }
        }
    }
}
