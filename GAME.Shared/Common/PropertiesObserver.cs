using System.ComponentModel;

namespace GAME.Shared.Common
{
    public class PropertiesObserver : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
     
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
