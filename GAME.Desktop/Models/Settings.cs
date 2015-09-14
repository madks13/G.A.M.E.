using GAME.Common.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Desktop.Models
{
    public class Settings : PropertiesObserver
    {
        public StringCollection PluginPaths
        {
            get { return Properties.Settings.Default.UserPluginPaths; }
            set 
            { 
                Properties.Settings.Default.UserPluginPaths = value;
                NotifyPropertyChanged("PluginPaths");
            }
        }

        public String PluginPattern
        {
            get { return Properties.Settings.Default.UserPluginPattern; }
            set 
            { 
                Properties.Settings.Default.UserPluginPattern = value;
                NotifyPropertyChanged("PluginPattern");
            }
        }
        public String PluginPath
        {
            get { return Properties.Settings.Default.UserPluginPath; }
            set
            {
                Properties.Settings.Default.UserPluginPath = value;
                NotifyPropertyChanged("PluginPath");
            }
        }

        public StringCollection LaunchedModules
        {
            get { return Properties.Settings.Default.UserLaunchedModules; }
            set 
            { 
                Properties.Settings.Default.UserLaunchedModules = value;
                NotifyPropertyChanged("LaunchedModules");
            }
        }
        public Boolean RememberModules
        {
            get { return Properties.Settings.Default.RememberOpenedModules; }
            set
            {
                Properties.Settings.Default.RememberOpenedModules = value;
                NotifyPropertyChanged("RememberModules");
            }
        }

        public Settings()
        {
            if (Properties.Settings.Default.UserPluginPaths == null)
                Properties.Settings.Default.UserPluginPaths = new StringCollection();
            if (Properties.Settings.Default.UserPluginPaths.Count == 0)
                Properties.Settings.Default.UserPluginPaths.Add(Properties.Settings.Default.DefaultPluginPath);
            if (String.IsNullOrEmpty(Properties.Settings.Default.UserPluginPattern))
                Properties.Settings.Default.UserPluginPattern = Properties.Settings.Default.DefaultPluginPattern;
            if (Properties.Settings.Default.UserLaunchedModules == null)
                Properties.Settings.Default.UserLaunchedModules = new StringCollection();
            if (String.IsNullOrEmpty(Properties.Settings.Default.UserPluginPath))
                Properties.Settings.Default.UserPluginPath = Properties.Settings.Default.DefaultPluginPath;
        }

        public void LoadDefaults()
        {
            //if (PluginPaths == null)
            //    PluginPaths = new StringCollection();
            //else
            //    PluginPaths.Clear();
            //PluginPaths.Add(Properties.Settings.Default.DefaultPluginPath);
            //PluginPattern = Properties.Settings.Default.DefaultPluginPattern;
            //PluginPath = Properties.Settings.Default.DefaultPluginPath;
            //if (LaunchedModules == null)
            //    LaunchedModules = new StringCollection();
            //else
            //    LaunchedModules.Clear();
            Properties.Settings.Default.Reset();
            NotifyPropertyChanged("PluginPath");
        }

        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
