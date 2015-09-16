using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.ComponentModel;

namespace GAME.Common.Core.Interfaces.Plugin
{
    [Flags]
    public enum PluginState
    {
        None,
        Loaded,
        Instantiated
    }
    public interface IPlugin
    {
        #region Properties

        String Type { get; }

        String SubType { get; }

        String Name { get; }

        String Version { get; }

        Boolean IsLaunched { get; }

        Boolean IsInstantiated { get; }

        #endregion

        #region Methods

        Tmod GetInstance<Tmod>() where Tmod : class;

        void UnloadModule();

        #endregion
    }
}
