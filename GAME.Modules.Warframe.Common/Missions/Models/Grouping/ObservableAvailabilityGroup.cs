using GAME.Common.Core.Models.Collections;
using System;

namespace GAME.Modules.Warframe.Common.Missions.Models.Grouping
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
