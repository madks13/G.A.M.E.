using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.ComponentModel;

namespace GAME.Common.Core.Interfaces.Plugin
{
    public interface IPlugin
    {
        #region Properties

        String Type { get; }

        String SubType { get; }

        String Name { get; }

        Boolean IsLaunched { get; }

        Boolean IsInstantiated { get; }

        #endregion

        #region Methods

        Tmod GetInstance<Tmod>() where Tmod : class;

        //CorePlugin GetInstance();

        void UnloadModule();

        #endregion
    }
}
