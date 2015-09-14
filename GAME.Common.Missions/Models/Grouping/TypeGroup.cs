using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAME.Common.Core.Models.Collections;
using GAME.Common.Missions.Interfaces;

namespace GAME.Common.Missions.Models.Grouping
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
