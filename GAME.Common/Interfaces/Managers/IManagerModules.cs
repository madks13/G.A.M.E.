using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using GAME.Common.Core.Interfaces.Plugin;
using GAME.Common.Core.Interfaces;

namespace GAME.Common.Core.Interfaces.Managers
{

    public interface IManagerModules
    {
        TMod GetModule<TMod>(String module) where TMod : class;

        TMod GetModule<TMod>(IPlugin plugin) where TMod : class;

        void AddPath(String path);

        void AddPaths(IEnumerable<String> paths, Boolean recursive = false);
        
        void AddPaths(IDictionary<String, Boolean> paths);

        void DelPath(String path);

        void DelPaths(IEnumerable<String> paths = null);

        void DelPaths(IDictionary<String, Boolean> paths);

        List<IPlugin> LoadModules(String pattern);

        void UnloadModules();
    }
}
