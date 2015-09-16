using GAME.Common.Core.ViewModels;
using GAME.Modules.Warframe.Common.Missions.Interfaces;
using GAME.Modules.Warframe.Common.Missions.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;

namespace GAME.Modules.Warframe.AlertScanner.Models
{
    public class OptionsData : PropertiesObserver
    {
        public int UserScanFrequency = 300;

        public int ScanFrequency
        {
            get { return Properties.Options.Default.UserScanFrequence; }
            set 
            { 
                Properties.Options.Default.UserScanFrequence = value;
                NotifyPropertyChanged("ScanFrequency");
            }
        }

        public String RssPC
        {
            get { return Properties.Options.Default.DefaultFeedPC; }
        }

        public String RssPS4
        {
            get { return Properties.Options.Default.DefaultFeedPS4; }
        }

        public String RssXBoxOne
        {
            get { return Properties.Options.Default.DefaultFeedXBoxOne; }
        }

        public int MinimumScanFrequency
        {
            get { return Properties.Options.Default.DefaultMinimumScanFrequence; }
        }
        public int MaximumScanFrequency
        {
            get { return Properties.Options.Default.DefaultMaximumScanFrequence; }
        }

        public int DefaultScanFrequency
        {
            get { return Properties.Options.Default.DefaultScanFrequence; }
        }

        public String NewActivitySoundPath
        {
            get { return Properties.Options.Default.UserNewActivitySound; }
            set { Properties.Options.Default.UserNewActivitySound = value; }
        }

        public GAME.Modules.Warframe.Common.Missions.Enums.Platforms Platforms
        {
            get { return (GAME.Modules.Warframe.Common.Missions.Enums.Platforms)Properties.Options.Default.UserSelectedPlatforms; }
            set
            {
                Properties.Options.Default.UserSelectedPlatforms = (int)value;
                NotifyPropertyChanged("Platforms");
            }
        }

        public StringCollection ExpandedExpanders
        {
            get { return Properties.Options.Default.UserSelectedExpanders; }
            set
            {
                Properties.Options.Default.UserSelectedExpanders = value;
                NotifyPropertyChanged("ExpandedExpanders");
            }
        }

        public StringCollection ViewedActivities
        {
            get { return Properties.Options.Default.UserViewedActivities; }
            set
            {
                Properties.Options.Default.UserViewedActivities = value;
                NotifyPropertyChanged("ViewedActivities");
            }
        }

        public OptionsData()
        {
            if (Properties.Options.Default.UserScanFrequence > MaximumScanFrequency
                || Properties.Options.Default.UserScanFrequence < MinimumScanFrequency)
                Properties.Options.Default.UserScanFrequence = DefaultScanFrequency;
            if (String.IsNullOrEmpty(Properties.Options.Default.UserNewActivitySound))
                Properties.Options.Default.UserNewActivitySound = Properties.Options.Default.DefaultNewActivitySound;
            if (Properties.Options.Default.UserSelectedPlatforms > 7 || Properties.Options.Default.UserSelectedPlatforms < 0)
                Properties.Options.Default.UserSelectedPlatforms = Properties.Options.Default.DefaultSelectedPlatforms;
            if (Properties.Options.Default.UserSelectedExpanders == null)
                Properties.Options.Default.UserSelectedExpanders = new StringCollection();
            if (Properties.Options.Default.UserViewedActivities == null)
                Properties.Options.Default.UserViewedActivities = new StringCollection();
        }

        public void LoadDefaults()
        {
            ScanFrequency = DefaultScanFrequency;
            ExpandedExpanders.Clear();
            Platforms =  GAME.Modules.Warframe.Common.Missions.Enums.Platforms.PC;
            ViewedActivities.Clear();
        }

        public void Save()
        {
            Properties.Options.Default.Save();
        }
    }
}
