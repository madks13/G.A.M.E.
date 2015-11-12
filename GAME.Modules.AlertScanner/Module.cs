using System;
using System.Windows.Controls;
using System.Runtime.Remoting;

using GAME.Common.Core.Plugin;
using GAME.Common.Core.Interfaces;
using GAME.Modules.Warframe.AlertScanner.Views;

namespace GAME.Modules.Warframe.AlertScanner
{
    public class Module : CoreModule
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ModuleWindow _mainWindow;

        public Module() : base("Alert Scanner")
        {
            ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            log.Info("Initializing");
            if (_mw == null)
            {
                log.Info("Instantiating module window");
                _mw = _mainWindow = new ModuleWindow(Name);
                _mainWindow.Closed += ModuleStopping;
            }
            _mw.Show();
        }

        public override String Name
        {
            get { return _name; }
        }

        public override void ShowMain()
        {
            log.Info("Showing main page");
            ShowMainWindow();
            _mainWindow.ShowPage(_mainWindow.FirstMainPage);
        }

        public override void Stop()
        {
            log.Info("Stopping module");
            if (_mainWindow != null)
            {
                log.Info("Closing module window");
                _mainWindow.Close();
                _mainWindow = null;
            }
            base.Stop();
        }

        public override void Hide()
        {
            log.Info("Hiding module window");
            base.Hide();
        }

        public override void ShowOptions()
        {
            log.Info("Showing options page");
            ShowMainWindow();
            _mainWindow.ShowPage(_mainWindow.FirstOptionsPage);
        }

        public override bool IsShowingMain
        {
            get { return _mainWindow.IsShowingMain; }
        }

        public override bool IsShowingOptions
        {
            get { return _mainWindow.IsShowingOptions; }
        }

        protected void ModuleStopping(object sender, EventArgs e)
        {
            log.Info("Stopping module");
            base.Stop();
        }
    }
}
