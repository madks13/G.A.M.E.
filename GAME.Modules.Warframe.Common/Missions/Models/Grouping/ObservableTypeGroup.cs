using GAME.Common.Core.Models.Collections;
using GAME.Modules.Warframe.Common.Missions.Interfaces;
using System;

namespace GAME.Modules.Warframe.Common.Missions.Models.Grouping
{
    public class ObservableTypeGroup : ObservableGroup<IActivity>
    {
        public Boolean Expanded { get; set; }

        public Boolean HasMarked
        {
            get
            {
                foreach (IActivity a in this)
                {
                    if (a.Marked)
                        return true;
                }
                return false;
            }
        }
        public ObservableTypeGroup(String groupName, String path = "")
            : base(groupName, path)
        {

        }
    }
}
