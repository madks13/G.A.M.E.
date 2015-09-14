using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using GAME.Common.Core.Interfaces.Plugin;
using GAME.Common.Core.Interfaces;
using System.Runtime.Remoting.Lifetime;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace GAME.Common.Plugin
{
    public class PluginAssemblyLoader : MarshalByRefObject
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //public List<IPlugin> GetAssembly(String path, Type pluginType)
        public List<Plugin.CorePlugin> GetAssembly(String path, Type pluginType)
        {
            log.Info("Attemptimng to load file " + path + " for the plugin type " + pluginType.Name);
            try
            {
                
                Assembly res = Assembly.Load(AssemblyName.GetAssemblyName(path));

                List<ObjRef> plugins = new List<ObjRef>();
                //List<IPlugin> plugs = new List<IPlugin>();
                List<Plugin.CorePlugin> plugs = new List<Plugin.CorePlugin>();
                var sponsor = new Sponsor();

                foreach (Type t in res.GetTypes())
                {
                    if (pluginType.IsAssignableFrom(t) && t != pluginType)
                    {
                        //IPlugin plugin = AppDomain.CurrentDomain.CreateInstanceAndUnwrap(t.Assembly.FullName, t.FullName) as IPlugin;
                        Plugin.CorePlugin plugin = AppDomain.CurrentDomain.CreateInstanceAndUnwrap(t.Assembly.FullName, t.FullName) as Plugin.CorePlugin;
                        if (plugin != null)
                        {
                            //ObjRef obj = RemotingServices.Marshal((MarshalByRefObject)plugin, null, typeof(IPlugin));
                            ObjRef obj = RemotingServices.Marshal((MarshalByRefObject)plugin, null, typeof(Plugin.CorePlugin));
                            //var lease = ((MarshalByRefObject)plugin).InitializeLifetimeService() as ILease;
                            //lease.Register(sponsor);
                            plugins.Add(obj);
                            plugs.Add(plugin);
                        }
                        else
                            log.Error("Returned plugin for type \""+ t.FullName + "\" was null");
                    }
                }
                log.Info("Loading file successfull");
                return plugs;
            }
            catch (Exception e)
            {
                log.Fatal("Plugin Assembly Loader : caught exception message : " + e.Message);
                return null;
                // throw new InvalidOperationException(ex);
            }
        }

        static Assembly FindAssemblyDependencies(object sender, ResolveEventArgs args)
        {
            String[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, args.Name);
            if (files.Length == 0)
                return null;
            return Assembly.LoadFile(files[0]);
        }
    }
}
