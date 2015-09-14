using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GAME.Common.Core.Interfaces;
using System.Collections.ObjectModel;

namespace GAME.Common.Core.Models.Collections
{
    public class ObservableGroup<Titem> : MTObservableCollection<Titem>, IGroup
    {
        protected String _group = "";
        protected String _pathToGroup = "";

        public ObservableGroup(String groupName, String path = "")
        {
            _group = groupName;
            _pathToGroup = path;
        }

        public String GroupName
        {
            get { return _group; }
        }

        public String PathToGroup
        {
            get { return _pathToGroup; }
        }
    }
}
