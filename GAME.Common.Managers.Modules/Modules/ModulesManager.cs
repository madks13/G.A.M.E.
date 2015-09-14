#region Usings

#region Usings Microsoft

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Windows;

#endregion

#region Usings Project

using GAME.Common.Core;
using GAME.Common.Core.Interfaces;
using GAME.Common.Core.Interfaces.Plugin;
using GAME.Common.Core.Interfaces.Managers;
using GAME.Common.Core.Interfaces.Tools;
using GAME.Common.Tools.Finder;
using System.Text;

#endregion

#endregion

namespace GAME.Common.Managers.Modules
{
    public class ManagerModules : IManagerModules
    {
        #region Fields

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IFinder _finder;
        private Dictionary<IPlugin, IPluginContainer> _plugins = new Dictionary<IPlugin, IPluginContainer>();
        private List<IPluginContainer> _pluginContainers = new List<IPluginContainer>();

        #endregion

        #region Methods

        #region C/D tor

        public ManagerModules(String path)
        {
            _finder = new Finder(path, true);
        }

        public ManagerModules(IEnumerable<String> paths)
        {
            if (paths == null || paths.Count() == 0)
            {
                throw new ArgumentException("ManagerModules : Path list empty");
            }
            _finder = new Finder(paths, true);
        }

        #endregion

        #region Loading

        private List<IPlugin> LoadModulesFromList(String pattern)
        {
            //Get all plugins with given name regexp
            log.Info("Initializing Finder");
            List<FileInfo> files = _finder.FindFiles(pattern);
            log.Info("Got " + files.Count + " files");
            //Iterate over results and load each file into separate container
            //Then store the container into a plugin list
            log.Info("Trying to load each file :");
            foreach (FileInfo file in files)
            {
                log.Info("Attempting to load file " + file.FullName);
                IPluginContainer pc = new PluginContainer();
                pc.LoadPlugin(file.FullName);
                if (pc.Modules.Count > 0)
                {
                    log.Info("Loading the plugin in the file " + file.FullName + " has succeeded");
                    pc.Modules.ForEach(p => _plugins[p] = pc);
                    _pluginContainers.Add(pc);
                }
                else
                    log.Info("Failed to load the plugin in the file " + file.FullName);
            }

            log.Info("Successfully loaded " + _plugins.Count + " files");

            //Return a list of all modules loaded
            return _plugins.Keys.ToList();
        }

        public List<IPlugin> LoadModules(String pattern)
        {
            log.Info("Begin loading modules");
            return LoadModulesFromList(pattern);
        }

        #endregion

        #region Settings

        public void AddPath(String path)
        {
            log.Info("Adding \"" + path + "\" to the paths");
            _finder.AddPath(path, true);
        }

        public void AddPaths(IEnumerable<string> paths, bool recursive = false)
        {
            log.Info("Adding " + paths.Count() + "paths to the paths");
            _finder.AddPaths(paths, recursive);
        }

        public void AddPaths(IDictionary<string, bool> paths)
        {
            log.Info("Adding paths from dictionnary");
            _finder.AddPaths(paths);
        }

        public void DelPath(string path)
        {
            log.Info("Deleting \"" + path + "\" from the paths");
            _finder.DelPath(path);
        }

        public void DelPaths(IEnumerable<string> paths = null)
        {
            if (paths == null)
                _finder.DelPaths();
            _finder.DelPaths(paths);
        }

        public void DelPaths(IDictionary<string, bool> paths)
        {
            _finder.DelPaths(paths);
        }

        #endregion

        #region Search

        private IPlugin FindPluginByName(String name)
        {
            log.Info("Searching for plugin \"" + name + "\"");
            return _plugins.Where(p => p.Key.Name == name).FirstOrDefault().Key;
        }

        private IPluginContainer FindPluginContainerByName(String name)
        {
            log.Info("Searching for plugin container \"" + name + "\"");
            return _plugins.Where(p => p.Key.Name == name).FirstOrDefault().Value;
        }

        #endregion

        #region Instantiation

        public TMod GetModule<TMod>(String module) where TMod : class
        {
            IPluginContainer pc = FindPluginContainerByName(module);
            return GetModule<TMod>(pc, module);
        }

        public TMod GetModule<TMod>(IPlugin plugin) where TMod : class
        {
            IPluginContainer pc = FindPluginContainerByName(plugin.Name);
            return GetModule<TMod>(pc, plugin.Name);
        }

        private TMod GetModule<TMod>(IPluginContainer pc, String name) where TMod : class
        {
            return pc.GetInstance<TMod>(name);
        }

        #endregion

        #endregion



        public void UnloadModules()
        {
            foreach (IPluginContainer pc in _pluginContainers)
            {
                pc.UnloadPlugin();
            }
            _pluginContainers.Clear();
        }
    }
}
