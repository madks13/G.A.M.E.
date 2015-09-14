using System.Windows.Controls;

using GAME.Modules.Warframe.AlertScanner.Models;
using System.ComponentModel;
using System.Windows.Input;
using System;
using System.Configuration;

namespace GAME.Modules.Warframe.AlertScanner.Views
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Page, INotifyPropertyChanged
    {
        private OptionsData _data;
        ApplicationSettingsBase _settings = Properties.Options.Default;

        public Options(OptionsData data)
        {
            InitializeComponent();
            _data = data;
            DataContext = _data;
        }

        public enum CloseReason
        {
            Unknown,
            Validation,
            Cancellation
        }

        public class ClosureEventArgs : EventArgs
        {
            public CloseReason Reason { get; set; }

            public ClosureEventArgs(CloseReason reason)
            {
                Reason = reason;
            }
        }

        protected virtual void RaiseOptionsClosed(ClosureEventArgs e)
        {
            EventHandler<ClosureEventArgs> handler = OptionsClosed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<ClosureEventArgs> OptionsClosed;

        private void ApplyChanges()
        {
            _data.Save();
        }

        private void OptionsOk_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ApplyChanges();
            RaiseOptionsClosed(new ClosureEventArgs(CloseReason.Validation));
        }

        private void OptionsApply_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ApplyChanges();
        }

        private void OptionsDefault_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _data.LoadDefaults();
        }

        private void OptionsCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RaiseOptionsClosed(new ClosureEventArgs(CloseReason.Cancellation));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
