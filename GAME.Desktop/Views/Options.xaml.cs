using GAME.Desktop.Models;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;


namespace GAME.Desktop.Views
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Page
    {
        private Settings _settings = null;

        public enum CloseReason
        {
            Unknown,
            Validation,
            Cancellation
        }

        public class ClosureEventArgs : EventArgs
        {
            public CloseReason Reason {get; set;}

            public ClosureEventArgs(CloseReason reason)
            {
                Reason = reason;
            }
        }

        public Options(Settings settings)
        {
            InitializeComponent();
            _settings = settings;
            DataContext = _settings;
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

        private void OptionsOk_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _settings.Save();
            RaiseOptionsClosed(new ClosureEventArgs(CloseReason.Validation));
        }

        private void OptionsApply_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _settings.Save();
        }

        private void OptionsDefault_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _settings.LoadDefaults();
        }

        private void OptionsCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RaiseOptionsClosed(new ClosureEventArgs(CloseReason.Cancellation));
        }

        private void ButtonChooseModulesFolder_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose the folder in which all the modules are stocked";
            fbd.ShowNewFolderButton = true;
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            DialogResult dr =  fbd.ShowDialog();
            if (dr == DialogResult.OK)
            {

                if (!Directory.Exists(fbd.SelectedPath))
                    return;
                _settings.PluginPath = fbd.SelectedPath;
                
            }
        }
    }
}
