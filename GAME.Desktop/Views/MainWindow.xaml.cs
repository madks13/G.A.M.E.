using GAME.Common.Core.Interfaces.Plugin;
using GAME.Common.Core.Managers;
using GAME.Common.Core.Models.Settings;
using GAME.Common.Core.Views;
using GAME.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace GAME.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : GAMEWindowCommon
    {
        #region Fields

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ObservableCollection<IPlugin> _modules = new ObservableCollection<IPlugin>();
        private Settings _settings = null;
        private ManagerModules _modmanager;
        private Boolean _disposed = false;
        //private Boolean _templateLoaded = false;

        #endregion

        #region Properties

        public ObservableCollection<IPlugin> Modules
        {
            get { return _modules; }
            set { _modules = value; }
        }

        #endregion

        #region Methods

        #region Initializers

        private void Init()
        {
            DataContext = this;
            _settings = new Settings("settings.xml");

            log.Info("Current Directory = " + Directory.GetCurrentDirectory());
            log.Info("Current Directory = " + Directory.GetCurrentDirectory());
            log.Info("Modules regexp = " + _settings["ModulePattern"].Value);
            log.Info("Module paths :");
            foreach (var path in _settings["ModuleFolders"].Value as List<String>)
            {
                log.Info("Modules path = " + path);
                log.Info("Modules absolute path = " + System.IO.Path.GetFullPath(path));
            }            
            //Initialize the module manager with path to plugins

            _modmanager = new ManagerModules(((List<String>)_settings["ModuleFolders"].Value).OfType<String>());
            List<IPlugin> l = _modmanager.LoadModules(_settings["ModulePattern"].Value.ToString());
            
            log.Info("Modules found :");
            l.ForEach(p => { log.Info(p.Name); _modules.Add(p); });
            log.Info("Total modules found : " + _modules.Count);

            FirstMainPage = new ModulesList(_settings, _modules);
            FirstOptionsPage = new GAMEOptions(_settings);
            
            MainBackground.Source = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Logos/GAME_logo_white.png", UriKind.Absolute));
            MainFrame.Content = FirstMainPage;            
        }

        #endregion

        #region C/D tor
        public MainWindow()
            : base("G.A.M.E. : Module Selector")
        {
            log.Info("========================================== Program Launched ==========================================");
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Init();
        }

        #endregion

        //private void OptionsClosed(object sender, Options.ClosureEventArgs e)
        //{
        //    ShowPage(FirstMainPage);
        //    if (!String.IsNullOrEmpty(_settings.PluginPath))
        //    {
        //        _modmanager.DelPaths();
        //        _modmanager.AddPath(_settings.PluginPath);
        //    }
        //}

        protected override void Dispose(Boolean disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                if (_modules != null)
                {
                    _modules.Clear();
                    _modules = null;
                }
                if (_modmanager != null)
                {
                    _modmanager.UnloadModules();
                    _modmanager = null;
                }
            }
            _disposed = true;
            base.Dispose(disposing);
        }

        #endregion
    }
}
