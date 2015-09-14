using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GAME.Common.Core.Interfaces.Plugin;

namespace GAME.Common.Core.Interfaces
{
    public interface IPluginGroup : IGroup
    {
        int CountLaunched { get; set; }
    }
}
