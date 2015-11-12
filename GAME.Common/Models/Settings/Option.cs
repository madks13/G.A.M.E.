using GAME.Common.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Common.Core.Models.Settings
{
    [Serializable]
    public class Option : IOption
    {
        #region DefaultValue

        private object _defaultObject = null;

        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]

        //[field: NonSerialized]
        public object DefaultValue
        {
            get { return _defaultObject; }
            set
            {
                _defaultObject = value;
                NotifyPropertyChanged("DefaultValue");
            }
        }

        #endregion

        #region Value

        private object _object = null;

        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]

        //[field: NonSerialized]
        public object Value
        {
            get { return _object; }
            set
            {
                if (DefaultValue == null)
                {
                    DefaultValue = value;
                    _object = value;
                    NotifyPropertyChanged("Object");
                }
                else if (DefaultValue.GetType().IsAssignableFrom(value.GetType()))
                {
                    _object = value;
                    NotifyPropertyChanged("Object");
                }
            }
        }

        #endregion

        #region Name

        private String _name = String.Empty;

        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        #endregion

        #region DisplayName

        private String _displayName = String.Empty;

        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
        public String DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                NotifyPropertyChanged("DisplayName");
            }
        }

        #endregion

        #region ShortInfo

        private String _shortInfo = String.Empty;

        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
        public String ShortInfo
        {
            get { return _shortInfo; }
            set
            {
                _shortInfo = value;
                NotifyPropertyChanged("ShortInfo");
            }
        }

        #endregion

        #region Info

        private String _info = String.Empty;

        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
        public String Info
        {
            get { return _info; }
            set
            {
                _info = value;
                NotifyPropertyChanged("Info");
            }
        }

        #endregion

        #region Group

        private String _group = String.Empty;

        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
        public String Group
        {
            get { return _group; }
            set
            {
                _group = value;
                NotifyPropertyChanged("Group");
            }
        }

        #endregion

        #region IsReadOnly

        private Boolean _isReadOnly = false;

        public Boolean IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }

            set
            {
                _isReadOnly = value;
                NotifyPropertyChanged("IsReadOnly");
            }
        }

        #endregion

        #region Change

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #endregion

        #region Reset

        public void Reset()
        {
            this.Value = this.DefaultValue;
        }

        #endregion
    }


    #region Unused

    //[Serializable]
    //public class GenOption<T> : IOption
    //{
    //    private T _object = default(T);

    //    [UserScopedSetting()]
    //    [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
    //    public T Value
    //    {
    //        get { return _object; }
    //        set
    //        {
    //            _object = value;
    //            NotifyPropertyChanged("Object");
    //        }
    //    }

    //    private String _name = String.Empty;

    //    [UserScopedSetting()]
    //    [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
    //    public String Name
    //    {
    //        get { return _name; }
    //        set
    //        {
    //            _name = value;
    //            NotifyPropertyChanged("Name");
    //        }
    //    }

    //    private String _displayName = String.Empty;

    //    [UserScopedSetting()]
    //    [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
    //    public String DisplayName
    //    {
    //        get { return _displayName; }
    //        set
    //        {
    //            _displayName = value;
    //            NotifyPropertyChanged("DisplayName");
    //        }
    //    }

    //    private String _shortInfo = String.Empty;

    //    [UserScopedSetting()]
    //    [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
    //    public String ShortInfo
    //    {
    //        get { return _shortInfo; }
    //        set
    //        {
    //            _shortInfo = value;
    //            NotifyPropertyChanged("ShortInfo");
    //        }
    //    }

    //    private String _info = String.Empty;

    //    [UserScopedSetting()]
    //    [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
    //    public String Info
    //    {
    //        get { return _info; }
    //        set
    //        {
    //            _info = value;
    //            NotifyPropertyChanged("Info");
    //        }
    //    }

    //    private String _group = String.Empty;

    //    [UserScopedSetting()]
    //    [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
    //    public String Group
    //    {
    //        get { return _group; }
    //        set
    //        {
    //            _group = value;
    //            NotifyPropertyChanged("Group");
    //        }
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //    public void NotifyPropertyChanged(string property)
    //    {
    //        if (PropertyChanged != null)
    //            PropertyChanged(this, new PropertyChangedEventArgs(property));
    //    }
    //}

#endregion
}
