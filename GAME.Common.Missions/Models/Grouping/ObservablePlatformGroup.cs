using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAME.Common.Core.Models.Collections;
using GAME.Common.Missions.Interfaces;

namespace GAME.Common.Missions.Models.Grouping
{
    public class ObservablePlatformGroup : ObservableGroup<ObservableTypeGroup>
    {
        public Boolean Expanded { get; set; }

        public int AlertCount
        {
            get
            {
                int c = 0;
                foreach (ObservableTypeGroup tg in this)
                    c += tg.Count;
                return c;
            }
        }

        public Boolean HasMarked
        {
            get
            {
                foreach (ObservableTypeGroup tg in this)
                {
                    if (tg.HasMarked)
                        return true;
                }
                return false;
            }
        }
        public ObservablePlatformGroup(String groupName, String path = "")
            : base(groupName, path)
        {

        }
    }
}
