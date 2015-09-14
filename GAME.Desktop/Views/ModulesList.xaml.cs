using GAME.Common.Core.Interfaces.Plugin;
using GAME.Common.Core.Models.Collections;
using GAME.Desktop.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace GAME.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ListTypes.xaml
    /// </summary>
    public partial class ModulesList : Page
    {
        private ObservablePluginGroup _plugins;
        private ObservableCollection<String> _types;
        private ObservableGroup<ObservablePluginGroup> _theList = new ObservableGroup<ObservablePluginGroup>("TheList", "");
        private Settings _settings = null;
        
        public ObservablePluginGroup Plugins
        {
            get { return _plugins; }
            set { _plugins = value; }
        }

        public ObservableCollection<String> Types
        {
            get { return _types; }
            set { _types = value; }
        }

        public ObservableGroup<ObservablePluginGroup> Groups
        {
            get { return _theList; }
            set { _theList = value; }
        }

        public ModulesList(Settings settings, ObservableCollection<IPlugin> plugins)
        {
            InitializeComponent();
            _plugins = new ObservablePluginGroup("Plugins", "");
            plugins.ToList().ForEach(_plugins.Add);
            _plugins.CollectionChanged += Update;
            PluginsToTypes();
            TypesToGroups();
            DataContext = this;
        }

        private void PluginsToTypes()
        {
            ObservableCollection<String> ls = new ObservableCollection<String>();
            foreach (var p in _plugins)
            {
                if (ls.Where(s => s == p.SubType).Count() == 0)
                    ls.Add(p.SubType);
            }
            _types = ls;
        }

        private void TypesToGroups()
        {
            foreach (var t in _types)
            {
                ObservablePluginGroup ng = new ObservablePluginGroup(t);
                _theList.Add(ng);
                _plugins.Where(p => p.SubType == t).ToList().ForEach(ng.Add);
            }
        }

        public void Update(Object o, EventArgs e)
        {
            PluginsToTypes();
            TypesToGroups();
        }

        private IPlugin GetPlugin(object sender)
        {
            Button b = sender as Button;
            Grid g = b.Parent as Grid;
            IPlugin p = g.DataContext as IPlugin;
            return p;
        }

        private void ModuleControlLaunch_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IPlugin plugin = GetPlugin(sender);
            IModule instance = plugin.GetInstance<IModule>();
            //var obj = instance as MarshalByRefObject;
            //var lease = obj.InitializeLifetimeService() as ILease;
            //if (lease.CurrentState != LeaseState.Active)
            //    lease.Renew(TimeSpan.FromMinutes(5));
            instance.ShowMain();
            //GetPlugin(sender).GetInstance<IModule>().ShowMain();
        }

        private void ModuleControlStop_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IPlugin plugin = GetPlugin(sender);
            //var pobj = ((MarshalByRefObject)plugin);
            //var please = pobj.InitializeLifetimeService() as ILease;
            //if (please.CurrentState != LeaseState.Active)
            //    please.Renew(TimeSpan.FromMinutes(5));
            IModule instance = plugin.GetInstance<IModule>();
            //var obj = instance as MarshalByRefObject;
            //var lease = obj.InitializeLifetimeService() as ILease;
            //if (lease.CurrentState != LeaseState.Active)
            //    lease.Renew(TimeSpan.FromMinutes(5));
            instance.Stop();
            //GetPlugin(sender).GetInstance<IModule>().Stop();
        }

        private void ModuleControlOptions_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IPlugin plugin = GetPlugin(sender);
            IModule instance = plugin.GetInstance<IModule>();
            //var obj = instance as MarshalByRefObject;
            //var lease = obj.InitializeLifetimeService() as ILease;
            //if (lease.CurrentState != LeaseState.Active)
            //    lease.Renew(TimeSpan.FromMinutes(5));
            instance.ShowOptions();
            //GetPlugin(sender).GetInstance<IModule>().ShowOptions();
        }

        private void ModuleControlHide_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IPlugin plugin = GetPlugin(sender);
            IModule instance = plugin.GetInstance<IModule>();
            //var obj = instance as MarshalByRefObject;
            //var lease = obj.InitializeLifetimeService() as ILease;
            //if (lease.CurrentState != LeaseState.Active)
            //    lease.Renew(TimeSpan.FromMinutes(5));
            instance.Hide();
            //GetPlugin(sender).GetInstance<IModule>().Hide();
        }
    }
}
