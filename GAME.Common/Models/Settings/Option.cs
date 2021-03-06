﻿using GAME.Common.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GAME.Common.Core.Models.Settings
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(List<String>))]
    [KnownType(typeof(DoubleInterval))]
    [KnownType(typeof(Brush))]
    public class Option : IOption
    {
        #region DefaultValue

        private object _defaultObject = null;

        [DataMember]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Binary)]
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

        [DataMember]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Binary)]
        public object Value
        {
            get { return _object; }
            set
            {
                if (DefaultValue == null)
                {
                    DefaultValue = value;
                    _object = value;
                    NotifyPropertyChanged("Value");
                }
                else if (DefaultValue.GetType().IsAssignableFrom(value.GetType()))
                {
                    _object = value;
                    NotifyPropertyChanged("Value");
                }
            }
        }

        #endregion

        #region Name

        private String _name = String.Empty;

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
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

        #region IsUnknownType

        private Boolean _isUnknownType = false;

        [DataMember]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
        public Boolean IsUnknownType
        {
            get
            {
                return _isUnknownType;
            }

            set
            {
                _isUnknownType = value;
                NotifyPropertyChanged("IsUnknownType");
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
}
