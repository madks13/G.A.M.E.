using GAME.Common.Core.Interfaces;
using GAME.Common.Core.Models.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace GAME.Common.Core.Views
{
    /// <summary>
    /// Interaction logic for GAMEOptions.xaml
    /// </summary>
    public partial class GAMEOptions : Page
    {
        private Options _options;
        private ObservableCollection<IGrouping<String, IOption>> _displayedOptions;
        private BrushConverter _brushConverter = new BrushConverter();

        public GAMEOptions(Options options)
        {
            InitializeComponent();
            _options = options;
            Init();
        }

        private void Init()
        {
            //IOption o = _options["ForegroundColor"];
            //if (o == null)
            //    _options.Add(new Option() { Name = "ForegroundColor", DisplayName = "Text color", Value = Brushes.White.ToString(), Group = "Themes", Info = "This will change the color of the text", ShortInfo = "Change text color" });
            //o = _options["BackgroundColor"];
            //if (o == null)
            //    _options.Add(new Option() { Name = "BackgroundColor", DisplayName = "Background color", Value = Brushes.Black.ToString(), Group = "Themes", Info = "This will change the color of the background", ShortInfo = "Change background color" });
            //o = _options["GroupBorderColor"];
            //if (o == null)
            //    _options.Add(new Option() { Name = "GroupBorderColor", DisplayName = "Groups border color", Value = Brushes.Gold.ToString(), Group = "Themes", Info = "This will change the color of the border for the groups", ShortInfo = "Change groups border color" });
            //o = _options["OptionBorderColor"];
            //if (o == null)
            //    _options.Add(new Option() { Name = "OptionBorderColor", DisplayName = "Options border color", Value = Brushes.Blue.ToString(), Group = "Themes", Info = "This will change the color of the border for the options", ShortInfo = "Change options border color" });
            //o = _options["OptionFontSize"];
            //if (o == null)
            //    _options.Add(new Option() { Name = "OptionFontSize", DisplayName = "OptionFontSize", Value = new DoubleInterval() { Maximum = 30, Minimum = 10, Value = 16 }, Group = "Themes", Info = "This will change the size of the text for the options", ShortInfo = "Change options text size" });
            RefreshDisplayedOptions();
        }

        private void RefreshDisplayedOptions()
        {
            var res = new ObservableCollection<IGrouping<string, IOption>>(_options.GroupBy(x => x.Group));
            _displayedOptions = res;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            DataContext = this; 
            //DataContext = _displayedOptions;
        }

        public ObservableCollection<IGrouping<String, IOption>> DisplayedOptions
        {
            get { return _displayedOptions; }
            set { _displayedOptions = value; }
        }

        public SolidColorBrush ForegroundColor
        {
            get
            {
                return (SolidColorBrush)_brushConverter.ConvertFromString(_options["ForegroundColor"].Value as String);
            }
            set
            {
                _options["ForegroundColor"].Value = value.Color.ToString();
            }
        }

        public SolidColorBrush BackgroundColor
        {
            get
            {
                return (SolidColorBrush)_brushConverter.ConvertFromString(_options["BackroundColor"].Value as String);
            }
            set
            {
                _options["BackgroundColor"].Value = value.Color.ToString();
            }
        }

        public SolidColorBrush GroupBorderColor
        {
            get
            {
                return (SolidColorBrush)_brushConverter.ConvertFromString(_options["GroupBorderColor"].Value as String);
            }
            set
            {
                _options["GroupBorderColor"].Value = value.Color.ToString();
            }
        }

        public SolidColorBrush OptionBorderColor
        {
            get
            {
                return (SolidColorBrush)_brushConverter.ConvertFromString(_options["OptionBorderColor"].Value as String);
            }
            set
            {
                _options["OptionBorderColor"].Value = value.Color.ToString();
            }
        }

        public DoubleInterval OptionFontSize
        {
            get
            {
                return (DoubleInterval)_options["OptionFontSize"].Value;
            }
            set
            {
                _options["OptionFontSize"].Value = value;
            }
        }

        private void NavigationButton_Ok_Click(object sender, RoutedEventArgs e)
        {
            _options.Save();
            NavigationService.GoBack();
        }

        private void NavigationButton_Apply_Click(object sender, RoutedEventArgs e)
        {
            _options.Save();
        }

        private void NavigationButton_Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        } 
    }
}
