using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace GAME.Common.Core.Interfaces.Plugin
{
    public interface IPluginContainer
    {
        Boolean LoadPlugin(String path);

        Boolean ReloadPlugin();

        Tmod GetInstance<Tmod>(String name) where Tmod : class;

        List<IPlugin> Modules { get; }

        void UnloadPlugin();
    }
}
