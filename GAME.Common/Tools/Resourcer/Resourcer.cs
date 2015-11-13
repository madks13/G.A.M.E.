using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAME.Common.Core.Tools.Downloader;

namespace GAME.Common.Core.Tools.Resourcer
{
    class Resourcer
    {
        private List<Resource> _resources = new List<Resource>();

        private String _rootFolder = String.Empty;

        public Resourcer(String rootFolder)
        {
            _rootFolder = rootFolder;
        }

        public Resource this[String id]
        {
            get
            {
                Resource r = _resources.Where(x => x.Id == id).First();
                if (r != null)
                {
                    if (!r.Exists)
                    {
                        GetResource(r);
                    }
                }
                else
                {
                    r = new Resource() { Id = id };
                }

                return r;
            }
            set
            {
                Resource r = _resources.Where(x => x.Id == id).First();
                if (r == null)
                {
                    _resources.Add(value);
                }
                else
                {
                    _resources[_resources.IndexOf(r)] = value;
                }
            }
        }

        private void GetResource(Resource r)
        {
            //Load object
            if (!r.Exists)
            {
                //Downloader d = new Downloader(r.Path, _rootFolder);
                
            }
        }

    }
}
