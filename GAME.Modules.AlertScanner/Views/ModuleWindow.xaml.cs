using GAME.Common.Core.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GAME.Modules.Warframe.AlertScanner.Views
{
    /// <summary>
    /// Interaction logic for BaseModuleWindow.xaml
    /// </summary>
    public partial class ModuleWindow : GAMEWindowMAO
    {

        #region Fields

        private ViewModels.AlertScanner _scanner;
        private Boolean disposed = false;

        #endregion

        #region Methods

        #region Initializers

        private void Init()
        {
            DataContext = this;
            _scanner = new ViewModels.AlertScanner();
            OptionsPage = new Views.Options(_scanner.Options_Data);
            MainPage = new Views.Main(_scanner.Main_Data, _scanner.Options_Data);
            ((Views.Main)MainPage).RaisedRefreshAsked += RefreshAlerts;
            MainFrame.Content = MainPage;
            Closing += ModuleWindow_Closing;
            //Closed += Close;
        }

        
        #endregion

        #region C/D tor

        public ModuleWindow(String name) : base("G.A.M.E. : " + name)
        {
            InitializeComponent();
            //Init();
        }

        #endregion

        #region Events

        #region Mouse events

        #region Click events

        #region Window events

        private void RefreshAlerts()
        {
            _scanner.Refresh();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Init();
        }
        void ModuleWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!disposed)
            {
                disposed = true;
                if (_main != null)
                    ((Views.Main)_main).Dispose();
                _scanner.Dispose();
                _scanner = null;
            }
        }

        private void OptionsClosed(object sender, Options.ClosureEventArgs e)
        {
            MainFrame.Content = _main;
        }

        private void WindowEvent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MainFrame.Content == _main)
                MainFrame.Content = _options;
            else
                MainFrame.Content = _main;
        }

        #endregion

        #region Move Events

        #endregion

        #endregion

        #endregion

        #endregion

        #endregion
    }

}
