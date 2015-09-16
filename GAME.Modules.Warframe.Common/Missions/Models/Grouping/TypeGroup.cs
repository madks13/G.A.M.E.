using GAME.Common.Core.Models.Collections;
using GAME.Modules.Warframe.Common.Missions.Interfaces;
using System;

namespace GAME.Modules.Warframe.Common.Missions.Models.Grouping
{
    public class TypeGroup : Group<IActivity>
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

        public TypeGroup(String groupName, String path = "")
            : base(groupName, path)
        {

        }

        ~TypeGroup()
        {
            this.Clear();
        }
    }
}
