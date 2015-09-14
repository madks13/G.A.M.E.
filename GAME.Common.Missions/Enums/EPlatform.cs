using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Common.Missions.Enums
{
    [Flags]
    public enum Platforms
    {
        None = 0,
        PC = 1,
        PS4 = 1 << 1,
        XB1 = 1 << 2
    }
}
