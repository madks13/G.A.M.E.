using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GAME.Common.Missions.Models;
using GAME.Common.Core.Models.Collections;
using GAME.Common.Core.Interfaces;

namespace GAME.Common.Missions.Models.Grouping
{
    public class AvailabilityGroup : Group<PlatformGroup>
    {
        public Boolean Expanded { get; set; }

        public int AlertCount
        {
            get
            {
                int c = 0;
                foreach (PlatformGroup pg in this)
                    c += pg.AlertCount;
                return c;
            }
        }

        public Boolean HasMarked
        {
            get
            {
                foreach (PlatformGroup pg in this)
                {
                    if (pg.HasMarked)
                        return true;
                }
                return false;
            }
        }
        public AvailabilityGroup(String groupName, String path = "")
            : base(groupName, path)
        {

        }
    }
}
