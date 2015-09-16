using GAME.Common.Core.Models.Collections;
using System;

namespace GAME.Modules.Warframe.Common.Missions.Models.Grouping
{
    public class PlatformGroup : Group<TypeGroup>
    {
        public Boolean Expanded { get; set; }

        public int AlertCount
        {
            get 
            { 
                int c = 0;
                foreach (TypeGroup tg in this)
                    c += tg.Count;
                return c;
            }
        }

        public Boolean HasMarked
        {
            get
            {
                foreach (TypeGroup tg in this)
                {
                    if (tg.HasMarked)
                        return true;
                }
                return false;
            }
        }

        public PlatformGroup(String groupName, String path = "")
            : base(groupName, path)
        {

        }

        ~PlatformGroup()
        {
            this.Clear();
        }
    }
}
