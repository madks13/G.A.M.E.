using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GAME.Common;
using GAME.Common.Core.Interfaces;
using GAME.Common.Core.Interfaces.Plugin;

namespace GAME.Common.Core.Models.Collections
{
    public class ObservablePluginGroup : ObservableGroup<IPlugin>, IPluginGroup
    {
        private int _countLaunched = 0;

        public int CountLaunched
        {
            get
            {
                if (_countLaunched == 0 && this.Count > 0)
                    _countLaunched = this.Where(p => p.IsLaunched).Count();
                return _countLaunched;
            }
            set
            {
                _countLaunched = value;
            }
        }

        public ObservablePluginGroup(String groupName, String path = "") : base(groupName, path)
        {

        }
    }
}
