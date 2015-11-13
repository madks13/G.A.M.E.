using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Common.Core.Tools.Resourcer
{
    class Resource : INotifyPropertyChanged
    {
        private String _id = String.Empty;

        public String Id 
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            } 
        }

        private String _url = String.Empty;

        public String URL
        {
            get { return _url; }
            set
            {
                _url = value;
                NotifyPropertyChanged("URL");
            }
        }

        private String _path = String.Empty;

        public String Path
        {
            get { return _path; }
            set
            {
                _path = value;
                NotifyPropertyChanged("Path");
            }
        }

        private Boolean _exists = false;

        public Boolean Exists
        {
            get { return _exists; }
            set
            {
                _exists = value;
                LastChanged = DateTime.UtcNow;
                NotifyPropertyChanged("Exists");
            }
        }

        private DateTime _lastChanged = default(DateTime);

        public DateTime LastChanged
        {
            get { return _lastChanged; }
            set
            {
                _lastChanged = value;
                NotifyPropertyChanged("LastChanged");
            }
        }

        public override bool Equals(object obj)
        {
            Resource r = obj as Resource;
            if (r == null)
                return false;
            return (r.Id == Id && r.Path == Path && r.URL == URL);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
