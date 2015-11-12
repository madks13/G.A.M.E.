using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Common.Core.Models.Settings
{
    [Serializable]
    public class Interval<T>
        where T : IComparable<T>
    {
        private T _maximum;
        private T _minimum;
        private T _value;

        public T Maximum
        {
            get { return _maximum; }
            set 
            {
                if (_minimum.CompareTo(value) > 0)
                {
                    _maximum = _minimum;
                    _minimum = value;
                }
                else
                {
                    _maximum = value;
                }
                    
            }
        }

        public T Minimum
        {
            get { return _minimum; }
            set
            {
                if (_maximum.CompareTo(value) < 0)
                {
                    _minimum = _maximum;
                    _maximum = value;
                }
                else
                {
                    _minimum = value;
                }
            }
        }

        public T Value
        {
            get { return _value; }
            set
            {
                if (_minimum.CompareTo(value) <= 0 && _maximum.CompareTo(value) >= 0)
                {
                    _value = value;
                }
                else if (_maximum.CompareTo(value) < 0)
                {
                    _value = _maximum;
                }
                else
                {
                    _value = _minimum;
                }
            }
        }
    }

    [Serializable]
    public class IntInterval : Interval<Int32>
    {

    }

    [Serializable]
    public class DecimalInterval : Interval<Decimal>
    {

    }

    [Serializable]
    public class DoubleInterval : Interval<Double>
    {

    }
}
