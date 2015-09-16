using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using GAME.Common.Plugin;
using GAME.Common.Core.Interfaces;
using GAME.Common.Core.Interfaces.Plugin;
using System.Linq;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Windows;
using System.Runtime.Remoting.Lifetime;
using System.Text;

namespace GAME.Common.Managers.Modules
{
    public class PluginContainer : IPluginContainer
    {
        #region Fields

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private String _path = "";
        private AppDomain _domain = null;
        private List<IPlugin> _modules = new List<IPlugin>();
        private List<Plugin.CorePlugin> _plugins = new List<Plugin.CorePlugin>();

        #endregion

        #region Information

        public string Domain
        {
            get
            {
                if (_domain != null)
                    return _domain.FriendlyName;
                else
                    return "No domains loaded";
            }
        }

        #endregion

        public PluginContainer()
        {
        }

        #region Methods

        public List<IPlugin> Modules
        {
            get
            {
                return _plugins.ConvertAll<IPlugin>(p => p as IPlugin);
                //return _modules;
            }
        }

        /// <summary>
        /// Loads an assembly into a separate domain as to avoid version conflicts between assembly dependencies with other assemblies
        /// </summary>
        /// <param name="path">The path to the assembly</param>
        /// <returns>Returns false if loading faile, otherwise returns true</returns>
        public Boolean LoadPlugin(String path)
        {
            //Check if path is valid
            if (String.IsNullOrEmpty(path) || !File.Exists(path))
            {
                log.Error("Plugin loader : path is empty or null");
                return false;
            }
            _path = path;
            log.Info("Begining attempt to load plugin in " + path);

            //Create domain with base directory in assembly directory
            //Activate shadow copying so files can be edited while they are used
            AppDomainSetup s = new AppDomainSetup();
            s.ApplicationName = Path.GetFileNameWithoutExtension(path);
            s.ApplicationBase = Path.GetDirectoryName(path);
            s.CachePath = Path.Combine(Path.GetDirectoryName(path), "cache" + Path.DirectorySeparatorChar);
            s.ShadowCopyFiles = "true";
            s.ShadowCopyDirectories = null;// Path.Combine(Path.GetDirectoryName(path), "sc" + Path.DirectorySeparatorChar);
            _domain = AppDomain.CreateDomain(Path.GetFileNameWithoutExtension(path), null, s);
            log.Info("Paths for domain " + s.ApplicationName + " are : ");
            log.Info("Application base : " + s.ApplicationBase);
            log.Info("Cache path : " + s.CachePath);
            log.Info("Shadow copy files : " + s.ShadowCopyFiles);
            log.Info("Shadow copy folders : " + s.ShadowCopyDirectories);
            if (_domain == null)
                log.Info("Creating a new domain failed for the path " + _path);
            
            //Instantiate a remote loader that will load the assembly into the plugin domain
            Type type = typeof(PluginAssemblyLoader);
            PluginAssemblyLoader loader = (PluginAssemblyLoader)_domain.CreateInstanceAndUnwrap(
                type.Assembly.FullName,
                type.FullName);
            if (loader == null)
                log.Info("Failed to initialize the PluginAssemblyLoader");
            
            //This will return all classes with IPlugin interface
            //Each can instantiate only one IModule
            List<Plugin.CorePlugin> list = loader.GetAssembly(path, typeof(Plugin.CorePlugin));
            if (list != null)
            {
                log.Info("Returned plugin list has " + list.Count + " elements");
                foreach (Plugin.CorePlugin p in list)
                {
                    if (p != null)
                    {
                        _plugins.Add(p);
                        log.Info("Added plugin " + p.Name);
                    }
                    else
                        log.Info("Plugin was null!");
                }
            }
            else
                log.Info("Returned plugin list was null");

            log.Info("Loading finished successfully");
            log.Info("End of attempt to load the plugin");
            //If we're here everything went well
            return true;
        }

        public Tmod GetInstance<Tmod>(String name) where Tmod : class
        {
            foreach (IPlugin plugin in _plugins)
            {
                if (plugin.Name == name)
                    return plugin.GetInstance<Tmod>(); ;
            }
            return null;
        }
        
        public void UnloadPlugin()
        {
            foreach (IPlugin p in _plugins)
            {
                
                if (p != null)
                    p.UnloadModule();
            }
            _plugins.Clear();
            _plugins = null;
            //_modules.Clear();
            //_modules = null;
            _domain = null;
            _path = null;
        }

        #endregion

        #region Callbacks

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

        //public void NotifyPropertyChanged(string propName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propName));
        //}

        //#endregion

        #endregion


        public bool ReloadPlugin()
        {
            if (String.IsNullOrEmpty(_path))
                return false;
            UnloadPlugin();
            return LoadPlugin(_path);
        }
    }
}
