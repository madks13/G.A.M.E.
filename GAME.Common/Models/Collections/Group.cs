using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GAME.Common.Core.Interfaces;

namespace GAME.Common.Core.Models.Collections
{
    public class Group<Titem> : List<Titem>, IGroup
    {
        protected String _group = "";
        protected String _pathToGroup = "";

        public Group(String groupName, String path = "")
        {
            _group = groupName;
            _pathToGroup = path;
        }

        public String GroupName
        {
            get { return _group; }
        }


        public string PathToGroup
        {
            get { return _pathToGroup; }
        }
    }
}
