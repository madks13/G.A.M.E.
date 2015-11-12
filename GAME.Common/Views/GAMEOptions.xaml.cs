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

namespace GAME.Common.Core.Views
{
    /// <summary>
    /// Interaction logic for GAMEOptions.xaml
    /// </summary>
    public partial class GAMEOptions : Page
    {
        private Options _options;

        public GAMEOptions(Options options)
        {
            InitializeComponent();
            _options = options;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _options;
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
