using GAME.Common.Core.Interfaces.Plugin;
using GAME.Common.Core.Views;
using GAME.Common.Managers.Modules;
using GAME.Desktop.Models;
using GAME.Common.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;

namespace GAME.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : GAMEWindowMAO
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
            _settings = new Settings();
            log.Info("Current Directory = " + Directory.GetCurrentDirectory());
            log.Info("Current Directory = " + Directory.GetCurrentDirectory());
            log.Info("Modules regexp = " + _settings.PluginPattern);
            log.Info("Modules path = " + _settings.PluginPath);
            log.Info("Modules absolute path = " + System.IO.Path.GetFullPath(_settings.PluginPath));
            
            //Initialize the module manager with path to plugins
            
            _modmanager = new ManagerModules(_settings.PluginPaths.OfType<String>());
            List<IPlugin> l = _modmanager.LoadModules(_settings.PluginPattern);
            
            log.Info("Modules found :");
            l.ForEach(p => { log.Info(p.Name); _modules.Add(p); });
            log.Info("Total modules found : " + _modules.Count);

            MainPage = new ModulesList(_settings, _modules);
            OptionsPage = new Options(_settings);
            
            ((Options)OptionsPage).OptionsClosed += OptionsClosed;
            
            MainBackground.Source = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Logos/GAME_logo_white.png", UriKind.Absolute));
            MainFrame.Content = MainPage;
            
            //Closed += Close;
            //GAMEWindowClosing += MainWindow_GAMEWindowClosing;
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

        private void OptionsClosed(object sender, Options.ClosureEventArgs e)
        {
            MainFrame.Content = MainPage;
            if (!String.IsNullOrEmpty(_settings.PluginPath))
            {
                _modmanager.DelPaths();
                _modmanager.AddPath(_settings.PluginPath);
            }
        }

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
