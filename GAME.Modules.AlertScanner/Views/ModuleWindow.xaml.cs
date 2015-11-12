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
    public partial class ModuleWindow : GAMEWindowCommon
    {

        #region Fields

        private ViewModels.AlertScanner _scanner;
        private Boolean _disposed = false;
        private Main _main;
        private Options _options;

        #endregion

        #region Methods

        #region Initializers

        private void Init()
        {
            DataContext = this;
            _scanner = new ViewModels.AlertScanner();
            FirstOptionsPage = _options = new Views.Options(_scanner.Options_Data);
            FirstMainPage = _main = new Views.Main(_scanner.Main_Data, _scanner.Options_Data);
            _main.RaisedRefreshAsked += RefreshAlerts;
            MainFrame.Content = FirstMainPage;
        }

        
        #endregion

        #region C/D tor

        public ModuleWindow(String name) : base("G.A.M.E. : " + name)
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void RefreshAlerts()
        {
            _scanner.Refresh();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Init();
        }

        #endregion

        #region Cleaning up

        protected override void Dispose(Boolean disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                if (_main != null)
                {
                    _main.Dispose();
                    _main = null;
                }
                if (_scanner != null)
                {
                    _scanner.Dispose();
                    _scanner = null;
                }
            }
            _disposed = true;
            base.Dispose(disposing);
        }

        #endregion

        #endregion
    }

}
