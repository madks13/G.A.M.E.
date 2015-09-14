using System;
using System.Windows.Controls;
using System.Runtime.Remoting;

using GAME.Common.Plugin;
using GAME.Common.Core.Interfaces;
using GAME.Modules.Warframe.AlertScanner.Views;

namespace GAME.Modules.Warframe.AlertScanner
{
    public class Module : CoreModule
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Module() : base("Alert Scanner")
        {
            IniializeAll();
        }

        private void IniializeAll()
        {
            log.Info("Initializing");
            if (_mw == null)
            {
                log.Info("Instantiating module window");
                _mw = new ModuleWindow(Name);
                ((ModuleWindow)_mw).Closed += ModuleStopping;
            }
            _mw.Show();
        }

        public override string Name
        {
            get { return _name; }
        }

        public override Boolean ShowMain()
        {
            log.Info("Showing main page");
            IniializeAll();
            return ((ModuleWindow)_mw).ShowMain();
        }

        public override Boolean Stop()
        {
            log.Info("Stopping module");
            if (_mw != null)
            {
                log.Info("Closing module window");
                ((ModuleWindow)_mw).Close();
            }
            return base.Stop();
        }

        public override Boolean Hide()
        {
            log.Info("Hiding module window");
            IniializeAll();
            return base.Hide();
        }

        public override Boolean ShowOptions()
        {
            log.Info("Showing options page");
            IniializeAll();
            return ((ModuleWindow)_mw).ShowOptions();
        }

        public override bool IsShowingMain
        {
            get { return ((ModuleWindow)_mw).IsShowingMain; }
        }

        public override bool IsShowingOptions
        {
            get { return ((ModuleWindow)_mw).IsShowingOptions; }
        }

        protected void ModuleStopping()
        {
            log.Info("Stopping module");
            base.Stop();
        }
    }
}
