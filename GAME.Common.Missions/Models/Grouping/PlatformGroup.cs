using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAME.Common.Missions.Models;
using GAME.Common.Core.Models.Collections;

namespace GAME.Common.Missions.Models.Grouping
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
