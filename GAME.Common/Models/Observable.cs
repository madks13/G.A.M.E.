using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Common.Core.Models
{
    public class ObservableConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
            CultureInfo culture, object @object, Type destinationType)
        {
            object objectValue = @object.GetType().GetProperty("Value").GetValue(@object, null);
            return base.ConvertTo(context, culture, objectValue, destinationType);
        }
    }

    [TypeConverter(typeof(ObservableConverter))]
    public class Observable<T> : INotifyPropertyChanged
    {
        T _object;
        Action _updateAction;

        public Observable(Action updateAction)
        {
            _updateAction = updateAction;
        }

        public T Object
        {
            get
            {
                return _object;
            }
            set 
            { 
                if (value == null || !value.Equals(_object))
                {
                    _object = value;
                    NotifyPropertyChanged("Object");
                    if (_updateAction != null)
                        _updateAction.Invoke();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public static implicit operator Observable<T>(T @object)
        {
            return new Observable<T>(null) { Object = @object };
        }
    }
}
