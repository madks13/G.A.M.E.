using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAME.Common.Missions.Models;
using GAME.Common.Core.Models.Collections;

namespace GAME.Common.Missions.Models.Grouping
{
    public class ObservableAvailabilityGroup : ObservableGroup<ObservablePlatformGroup>
    {
        public Boolean Expanded { get; set; }

        public int AlertCount
        {
            get
            {
                int c = 0;
                foreach (ObservablePlatformGroup tg in this)
                    c += tg.AlertCount;
                return c;
            }
        }

        public Boolean HasMarked
        {
            get
            {
                foreach (ObservablePlatformGroup pg in this)
                {
                    if (pg.HasMarked)
                        return true;
                }
                return false;
            }
        }
        public ObservableAvailabilityGroup(String groupName, String path = "")
            : base(groupName, path)
        {

        }
    }
}
