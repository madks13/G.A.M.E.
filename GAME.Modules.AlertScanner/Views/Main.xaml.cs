using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using GAME.Common.Core.Interfaces.Tools;
using GAME.Modules.Warframe.AlertScanner.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;
using GAME.Common.Missions.Interfaces;
using GAME.Common.Core.Models;
using GAME.Common.Core.Models.Collections;
using System.ComponentModel;
using System;
using System.Linq;
using GAME.Common.Missions.Models.Grouping;
using System.Windows.Media.Imaging;
using GAME.Common.Core.Interfaces;
using System.Collections.Specialized;
using System.Reflection;
using System.Windows.Threading;
using GAME.Common.Core.Views;

namespace GAME.Modules.Warframe.AlertScanner.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page //GAMEPageBase
    {
        #region Enums

        private enum ImageIndex
        {
            PC_I,
            PC_A,
            PS4_I,
            PS4_A,
            XB1_I,
            XB1_A
        }

        #endregion

        #region Delegates

        public delegate void RefreshAlertsAsked();

        #endregion

        #region Fields

        private MainData _mData;
        private OptionsData _oData;

        private Dictionary<String, Boolean> _expandedStates = new Dictionary<String, Boolean>();
        
        private List<IActivity> _timedActivities = new List<IActivity>();
        private ObservableGroup<ObservableAvailabilityGroup> _sortedActivities = new ObservableGroup<ObservableAvailabilityGroup>("SortedActivities");
        private Boolean _newActivities = false;

        private Dictionary<ImageIndex, BitmapImage> _images = new Dictionary<ImageIndex, BitmapImage>();
        
        private Boolean _disposed = false;
        private DispatcherTimer _scanTimer = null;
        private DispatcherTimer _secondTimer = null;
        private MediaPlayer _mediaPlayer = null;

        #endregion

        #region Properties

        public ObservableGroup<ObservableAvailabilityGroup> SortedActivities
        {
            get { return _sortedActivities; }
            set { _sortedActivities = value; }
        }

        #endregion

        private void Init()
        {
            _images[ImageIndex.PC_A] = new BitmapImage(new Uri("pack://application:,,,/GAME.Modules.Warframe.AlertScanner;component/Resources/Images/Buttons/Platforms/Default_Icon_PC_active.png", UriKind.Absolute));
            _images[ImageIndex.PC_I] = new BitmapImage(new Uri("pack://application:,,,/GAME.Modules.Warframe.AlertScanner;component/Resources/Images/Buttons/Platforms/Default_Icon_PC_inactive.png", UriKind.Absolute));
            _images[ImageIndex.PS4_A] = new BitmapImage(new Uri("pack://application:,,,/GAME.Modules.Warframe.AlertScanner;component/Resources/Images/Buttons/Platforms/Default_Icon_PS4_active.png", UriKind.Absolute));
            _images[ImageIndex.PS4_I] = new BitmapImage(new Uri("pack://application:,,,/GAME.Modules.Warframe.AlertScanner;component/Resources/Images/Buttons/Platforms/Default_Icon_PS4_inactive.png", UriKind.Absolute));
            _images[ImageIndex.XB1_A] = new BitmapImage(new Uri("pack://application:,,,/GAME.Modules.Warframe.AlertScanner;component/Resources/Images/Buttons/Platforms/Default_Icon_XB1_active.png", UriKind.Absolute));
            _images[ImageIndex.XB1_I] = new BitmapImage(new Uri("pack://application:,,,/GAME.Modules.Warframe.AlertScanner;component/Resources/Images/Buttons/Platforms/Default_Icon_XB1_inactive.png", UriKind.Absolute));
            
            _scanTimer = new DispatcherTimer(DispatcherPriority.Normal);
            _scanTimer.Interval = TimeSpan.FromSeconds(_oData.UserScanFrequency);
            _scanTimer.Tick += _scanTimer_Tick;
            _scanTimer.Start();

            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.MediaOpened += mp_MediaOpened;
            _mediaPlayer.MediaEnded += mp_MediaEnded;
        }

        #region Ctor

        public Main(MainData mData, OptionsData oData)
        {
            InitializeComponent();

            _mData = mData;
            _mData.PropertyChanged += OnActivitiesUpdated;
            _oData = oData;
            _oData.PropertyChanged += OnDataUpdated;
            
            Init();
            
            UpdateActivitesList();
            UpdatePlatforms();
            DataContext = this;
        }

        #endregion

        public RefreshAlertsAsked RaisedRefreshAsked;

        private void UpdateActivitesList()
        {
            _sortedActivities.Clear();
            _timedActivities.Clear();

            StringCollection viewed = _oData.ViewedActivities;
            _oData.ViewedActivities = new StringCollection();

            foreach (IActivity a in _mData.Activities)
            {
                if (a.Type == (Enum)GAME.Common.Missions.Enums.Type.Alert)
                    _timedActivities.Add(a);
                String pName = Enum.GetName(a.Platform.GetType(), a.Platform);
                String tName = Enum.GetName(a.Type.GetType(), a.Type);
                String aName = a.Done ? "Done" : "Available";
                if (!a.Viewed)
                    _newActivities = true;
                if (viewed.Contains(a.Id))
                    _oData.ViewedActivities.Add(a.Id);

                ObservableAvailabilityGroup availabilityGroup = _sortedActivities.Where(g => g.GroupName == aName).FirstOrDefault();
                if (availabilityGroup == null)
                {
                    availabilityGroup = new ObservableAvailabilityGroup(aName, _sortedActivities.PathToGroup + aName);
                    _sortedActivities.Add(availabilityGroup);
                }
                availabilityGroup.Expanded = (_oData.ExpandedExpanders.Contains(availabilityGroup.PathToGroup)? true : false);
                
                ObservablePlatformGroup platformGroup = availabilityGroup.Where(g => g.GroupName == pName).FirstOrDefault();
                if (platformGroup == null)
                {
                    platformGroup = new ObservablePlatformGroup(pName, availabilityGroup.PathToGroup + pName);
                    availabilityGroup.Add(platformGroup);
                }
                platformGroup.Expanded = (_oData.ExpandedExpanders.Contains(platformGroup.PathToGroup) ? true : false);

                ObservableTypeGroup typeGroup = platformGroup.Where(g => g.GroupName == tName).FirstOrDefault();
                if (typeGroup == null)
                {
                    typeGroup = new ObservableTypeGroup(tName, platformGroup.PathToGroup + tName);
                    platformGroup.Add(typeGroup);
                }
                typeGroup.Expanded = (_oData.ExpandedExpanders.Contains(typeGroup.PathToGroup) ? true : false);

                typeGroup.Add(a);
            }
            _sortedActivities.OrderBy(g => g.GroupName);
            foreach (var availabilityGroup in _sortedActivities)
            {
                availabilityGroup.OrderBy(g => g.GroupName);
                foreach (var platformGroup in availabilityGroup)
                {
                    platformGroup.OrderBy(g => g.GroupName);
                    foreach (var typeGroup in platformGroup)
                    {
                        if (typeGroup.GroupName == Enum.GetName(typeof(GAME.Common.Missions.Enums.Type), GAME.Common.Missions.Enums.Type.Alert))
                            typeGroup.OrderBy(a => ((GAME.Common.Missions.Models.Activity.Alert)a).TimeLeft);
                        else
                            typeGroup.OrderBy(a => a.PublishDate);
                    }
                }
            }
            if (_newActivities)
            {
                Uri u = new Uri(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + _oData.NewActivitySoundPath), UriKind.Absolute);
                _mediaPlayer.Open(u);
                _newActivities = false;
            }
            viewed.Clear();
        }

        private void Rescan()
        {
            if (RaisedRefreshAsked != null)
                RaisedRefreshAsked();
        }

        private void _scanTimer_Tick(object sender, EventArgs e)
        {
            Rescan();
        }

        private void UpdatePlatforms()
        {
            LogoPC.Content = _images[(_oData.Platforms.HasFlag(GAME.Common.Missions.Enums.Platforms.PC) ? ImageIndex.PC_A : ImageIndex.PC_I)];
            LogoPS4.Content = _images[(_oData.Platforms.HasFlag(GAME.Common.Missions.Enums.Platforms.PS4) ? ImageIndex.PS4_A : ImageIndex.PS4_I)];
            LogoXB1.Content = _images[(_oData.Platforms.HasFlag(GAME.Common.Missions.Enums.Platforms.XB1) ? ImageIndex.XB1_A : ImageIndex.XB1_I)];
        }

        private void Refresh_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Rescan();
        }

        private void AddRemovePlaform(GAME.Common.Missions.Enums.Platforms platform)
        {
            _oData.Platforms ^= platform;
        }

        private void PlatformButton_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Button b = sender as Button;
            if (b.Name == LogoPC.Name)
                AddRemovePlaform(GAME.Common.Missions.Enums.Platforms.PC);
            if (b.Name == LogoPS4.Name)
                AddRemovePlaform(GAME.Common.Missions.Enums.Platforms.PS4);
            if (b.Name == LogoXB1.Name)
                AddRemovePlaform(GAME.Common.Missions.Enums.Platforms.XB1);
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander ex = sender as Expander;
            Label l = ex.Header as Label;
            IGroup g = l.Content as IGroup;
            if (!_oData.ExpandedExpanders.Contains(g.PathToGroup))
                _oData.ExpandedExpanders.Add(g.PathToGroup);
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            Expander ex = sender as Expander;
            Label l = ex.Header as Label;
            IGroup g = l.Content as IGroup;
            if (_oData.ExpandedExpanders.Contains(g.PathToGroup))
                _oData.ExpandedExpanders.Remove(g.PathToGroup);
        }

        private void Label_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Label l = sender as Label;
            IActivity a = l.DataContext as IActivity;
            a.Viewed = true;
            _oData.ViewedActivities.Add(a.Id);
        }

        //protected override void Dispose(Boolean disposing)
        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                if (_mediaPlayer != null && (_mediaPlayer.HasAudio || _mediaPlayer.HasVideo))
                {
                    _mediaPlayer.Close();
                    _mediaPlayer = null;
                }
            }
            if (_scanTimer != null)
            {
                _scanTimer.Stop();
                _scanTimer = null;
            }
            if (_secondTimer != null)
            {
                _secondTimer.Stop();
                _secondTimer = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void OnActivitiesUpdated(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Activities")
                UpdateActivitesList();
        }

        public void OnDataUpdated(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ScanFrequency")
                _scanTimer.Interval = TimeSpan.FromSeconds(_oData.ScanFrequency);
            if (e.PropertyName == "Platforms")
                UpdatePlatforms();
        }

        void mp_MediaOpened(object sender, EventArgs e)
        {
            _mediaPlayer.Play();
        }

        void mp_MediaEnded(object sender, EventArgs e)
        {
            _mediaPlayer.Close();
        }
    }
}
