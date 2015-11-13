using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAME.Common.Core.Tools.Serializer;
using GAME.Common.Core.Interfaces;

namespace GAME.Common.Core.Models.Settings
{
    public class Options : ObservableCollection<IOption>
    {
        protected String _path = String.Empty;

        public Options()
        {
        }

        public Options(String path)
        {
            if (!LoadFromFile(path))
            {
                if (!String.IsNullOrEmpty(path))
                {
                    _path = path;
                }
                else
                {
                    _path = "settings.xml";
                }
                BuildDefaultSettings();
            }
        }

        public Boolean LoadFromFile(String path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            _path = path;

            //Debug
            //String extention = Path.GetExtension(path);
            //String fileName = Path.GetFileNameWithoutExtension(path);
            //String pathToFile = Path.GetDirectoryName(path);

            try
            {
                var result = Serializer<List<Option>>.Deserialize(path);
                this.AddRange(result);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Boolean Load()
        {
            if (String.IsNullOrEmpty(_path))
            {
                return false;
            }
            return LoadFromFile(_path);
        }

        public Boolean SaveToFile(String path)
        {
            _path = path;

            //Debug
            //String extention = Path.GetExtension(path);
            //String fileName = Path.GetFileNameWithoutExtension(path);
            //String pathToFile = Path.GetDirectoryName(path);

            try
            {
                //Type[] types = this
                //                    //.Where(x => x.IsUnknownType)
                //                    .Select(x => x.Value.GetType())
                //                    .Distinct()
                //                    .ToArray();
                Serializer<List<Option>>.Serialize(path, this.Cast<Option>().ToList());
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Boolean Save()
        {
            if (String.IsNullOrEmpty(_path))
            {
                return false;
            }
            return SaveToFile(_path);
        }

        public Options(IEnumerable<Option> options)
        {
            if (options != null)
            {
                foreach(var option in options)
                {
                    this.Add(option);
                }
            }
        }

        public IOption this[String key]
        {
            get
            {
                return this.Where(x => x.Name == key).FirstOrDefault();
            }
            set
            {
                Option res = null;
                try
                {
                    this.First(x => x.Name == key);
                }
                catch
                {
                    //Nothing to do see here
                }
                if (res != null)
                {
                    this.SetItem(this.IndexOf(res), value);
                }
                else
                    this.Add(value);
            }
        }

        public Options AddRange(IEnumerable<IOption> options)
        {
            if (options != null)
            {
                foreach(var option in options)
                {
                    this.Add(option);
                }
            }
            return this;
        }

        public new void Add(Option item)
        {
            var res = this.Where(x => x.Name == item.Name).FirstOrDefault();
            if (res != null)
            {
                this.Remove(res);
            }
            base.Add(item);
        }

        public void ResetAll()
        {
            this.ToList().ForEach(x => x.Reset());
        }

        protected virtual void BuildDefaultSettings()
        {

        }
    }
}
