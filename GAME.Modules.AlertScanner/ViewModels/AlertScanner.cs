using GAME.Common.Core.ViewModels;
using GAME.Modules.Warframe.Common.Missions.Interfaces;
using GAME.Modules.Warframe.Common.Missions.Models.Activity;
using GAME.Modules.Warframe.AlertScanner.Models;
using GAME.Modules.Warframe.Common.Managers.RSS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GAME.Modules.Warframe.AlertScanner.ViewModels
{
    class AlertScanner : PropertiesObserver, IDisposable
    {
        #region Enums

        private enum DataSource
        {
            RSS,
            DeathSnack
        }

        #endregion

        #region Fields

        private MainData _mData;
        private OptionsData _oData;
        private RSSManager _rssManager;
        private Boolean _disposed = false;

        #endregion

        #region Properties

        public MainData Main_Data
        {
            get { return _mData; }
            set { _mData = value; }
        }

        public OptionsData Options_Data
        {
            get { return _oData; }
            set { _oData = value; }
        }

        #endregion

        public void Refresh()
        {
            UpdateAlerts();
        }

        #region Parsing

        private List<IActivity> FeedToActivity(List<FeedDTO> feeds, Enum platform)
        {
            List<IActivity> lst = new List<IActivity>();
            foreach (FeedDTO f in feeds)
            {
                IActivity activity = _mData.Activities.Where(i => i.Id == f.Id).FirstOrDefault();
                if (activity == null)
                {
                    switch ((GAME.Modules.Warframe.Common.Missions.Enums.Type)System.Enum.Parse(typeof(GAME.Modules.Warframe.Common.Missions.Enums.Type), f.Author, true))
                    {
                        case GAME.Modules.Warframe.Common.Missions.Enums.Type.Alert:
                            //Information to set different from other activities
                            //It is separated because of the instantiation of different classes
                            activity = new Alert(f.Title);

                            ((Alert)activity).Description = f.Description;
                            ((Alert)activity).ExpirationDate = f.ExpireDate;
                            //a.Faction = (E_Factions)System.Enum.Parse(typeof(E_Factions), f.Faction.Split('_')[1], true);
                            break;
                        case GAME.Modules.Warframe.Common.Missions.Enums.Type.Outbreak:
                            activity = new Outbreak(f.Title);
                            break;
                        case GAME.Modules.Warframe.Common.Missions.Enums.Type.Invasion:
                            activity = new Invasion(f.Title);
                            break;
                        default:
                            break;
                    }

                    activity.Id = f.Id;
                    activity.PublishDate = f.PublishDate;
                    activity.Platform = platform;
                    activity.Viewed = _oData.ViewedActivities.Contains(activity.Id);
                    activity.Done = false;
                    activity.Marked = false;
                }
                //a.PropertyChanged += ActivityStatusChanged; //This is used to ref

                //Add the activity to the list
                lst.Add(activity);                
            }
            return lst;
        }

        #endregion

        private async void UpdateAlerts()
        {
            List<IActivity> al = new List<IActivity>();
            if (_oData.Platforms.HasFlag(GAME.Modules.Warframe.Common.Missions.Enums.Platforms.PC))
                    al.AddRange(FeedToActivity(await _rssManager.GetFeeds(_oData.RssPC), GAME.Modules.Warframe.Common.Missions.Enums.Platforms.PC));
            if (_oData.Platforms.HasFlag(GAME.Modules.Warframe.Common.Missions.Enums.Platforms.PS4))
                al.AddRange(FeedToActivity(await _rssManager.GetFeeds(_oData.RssPS4), GAME.Modules.Warframe.Common.Missions.Enums.Platforms.PS4));
            if (_oData.Platforms.HasFlag(GAME.Modules.Warframe.Common.Missions.Enums.Platforms.XB1))
                al.AddRange(FeedToActivity(await _rssManager.GetFeeds(_oData.RssXBoxOne), GAME.Modules.Warframe.Common.Missions.Enums.Platforms.XB1));
            _mData.Activities = al;
        }

        #region Ctor

        public AlertScanner()
        {
            _mData = new MainData();
            _mData.PropertyChanged += OnDataUpdated;

            _oData = new OptionsData();
            _oData.PropertyChanged += OnDataUpdated;

            _rssManager = new RSSManager();

            UpdateAlerts();
        }

        #endregion

        public void OnDataUpdated(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Platforms")
                UpdateAlerts();
        }

        #region Dtor

        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
            }
            if (_mData != null)
                _mData = null;
            if (_oData != null)
            {
                _oData.Save();
                _oData = null;
            }
            if (_rssManager != null)
            {
                _rssManager = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
