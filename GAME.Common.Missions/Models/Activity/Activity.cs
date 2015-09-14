using System;

using GAME.Common.Missions.Interfaces;
using GAME.Common.Core.ViewModels;

namespace GAME.Common.Missions.Models.Activity
{
    public abstract class Activity : PropertiesObserver, IActivity
    {
        protected Enum _type;

        public Enum Type 
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged("Type"); }
        }

        protected String _id;

        public String Id 
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged("Id"); }
        }

        protected String _info;

        public String Info 
        {
            get { return _info; }
            set { _info = value; NotifyPropertyChanged("Info"); }
        }

        protected DateTimeOffset _publishDate;

        public DateTimeOffset PublishDate 
        {
            get { return _publishDate; }
            set { _publishDate = value; NotifyPropertyChanged("PublishDate"); }
        }

        protected Boolean _done;

        public Boolean Done 
        {
            get { return _done; }
            set { _done = value; NotifyPropertyChanged("Done"); }
        }

        protected Boolean _viewed;

        public Boolean Viewed 
        {
            get { return _viewed; }
            set { _viewed = value; NotifyPropertyChanged("Viewed"); }
        }

        protected Boolean _marked;

        public Boolean Marked
        {
            get { return _marked; }
            set { _marked = value; NotifyPropertyChanged("Marked"); }
        }

        protected Enum _platform;

        public Enum Platform 
        {
            get { return _platform; }
            set { _platform = value; NotifyPropertyChanged("Platform"); }
        }

        public Activity(String info)
        {
            Info = info;
        }
    }
}
