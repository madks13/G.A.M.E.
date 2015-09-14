using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.ServiceModel.Syndication;
using System.Timers;
using System.ComponentModel;

using GAME.Shared.Models;
using GAME.Shared.Models.Groups;
using GAME.Shared.Models.Activities;
using GAME.Shared.Interfaces;

namespace GAME.Shared.ViewModels
{
    public class RSSManager : BaseViewModel
    {
        #region Resources
        private bool _firstUpdate = true;
        private string _uriLeft = "http://content.";
        private string _uriRight = "warframe.com/dynamic/rss.php";
        private Timer _timer;
        private Timer _oneSecondTimer;

        private E_Platforms _platformsList = E_Platforms.PC;
       
        public E_Platforms PlatformsList
        {
            get { return _platformsList; }
            set
            {
                _platformsList = value;
                NotifyPropertyChanged("PlatformsList");
            }
        }

        private List<IActivity> _mainActivitiesList = new List<IActivity>();

        public List<IActivity> MainActivitiesList
        {
            get { return _mainActivitiesList; }
            set
            {
                _mainActivitiesList = value;
                NotifyPropertyChanged("MainActivitiesList");
            }
        }

        private ObservableCollection<IActivity> _activitiesList = new ObservableCollection<IActivity>();

        public ObservableCollection<IActivity> ActivitiesList
        {
            get { return _activitiesList; }
            set
            {
                _activitiesList = value;
                NotifyPropertyChanged("ActivitiesList");
            }
        }

        private ObservableCollection<IActivity> _alertsList = new ObservableCollection<IActivity>();

        public ObservableCollection<IActivity> AlertsList
        {
            get { return _alertsList; }
            set
            {
                _alertsList = value;
                NotifyPropertyChanged("AlertsList");
            }
        }
       
        private ObservableCollection<IActivity> _invasionsList = new ObservableCollection<IActivity>();

        public ObservableCollection<IActivity> InvasionsList
        {
            get { return _invasionsList; }
            set
            {
                _invasionsList = value;
                NotifyPropertyChanged("InvasionsList");
            }
        }
        
        private ObservableCollection<IActivity> _outbreaksList = new ObservableCollection<IActivity>();

        public ObservableCollection<IActivity> OutbreaksList
        {
            get { return _outbreaksList; }
            set
            {
                _outbreaksList = value;
                NotifyPropertyChanged("OutbreaksList");
            }
        }

        private ObservableCollection<IActivity> _doneList = new ObservableCollection<IActivity>();

        public ObservableCollection<IActivity> DoneList
        {
            get { return _doneList; }
            set
            {
                _doneList = value;
                NotifyPropertyChanged("DoneList");
            }
        }

        private uint _refreshRate = 300;

        public uint RefreshRate
        {
            get { return _refreshRate; }
            set
            {
                _refreshRate = value;
                _timer.Interval = _refreshRate * 1000;
                _timerElapses = DateTime.UtcNow.AddSeconds(_refreshRate);
                NotifyPropertyChanged("RefreshRate");
            }
        }

        private DateTime _timerElapses;

        private string _alertNewSound = null;

        public string AlertNewSound
        {
            get { return _alertNewSound; }
            set
            {
                _alertNewSound = value;
                NotifyPropertyChanged("AlertNewSound");
            }
        }

        private string _alertFlagSound = null;

        public string AlertFlagSound
        {
            get { return _alertFlagSound; }
            set
            {
                _alertFlagSound = value;
                NotifyPropertyChanged("AlertFlagSound");
            }
        }

        public String TimerElapses
        {
            get 
            {
                TimeSpan t = _timerElapses - DateTime.UtcNow;
                return ((t.Hours > 0 ? t.Hours + ":" : "") + (t.Minutes > 0 ? t.Minutes + ":" : "") + t.Seconds);
            }
        }

        #endregion

        Uri _newSound;
        Uri _alertSound;
        
        public RSSManager()
        {            
            _platformsList = E_Platforms.PC;
            
            PropertyChanged += MainListChanged;
            
            Update();

            _firstUpdate = false;

            _timer = new Timer(RefreshRate * 1000);
            _timer.Elapsed += timer_Elapsed;
            _timer.Start();
            _timerElapses = DateTime.UtcNow.AddSeconds(_refreshRate);

            _oneSecondTimer = new Timer(1000);
            _oneSecondTimer.Elapsed += timer_Elapses;
            _oneSecondTimer.Start();

            
            _alertSound = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Default_Alert_Alarm.mp3");
            _newSound = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Default_Alert_New.mp3"); 
            //LoadSoundResource(Properties.Resources.Default_Alert_Alarm, _alertSound);
            //LoadSoundResource(Properties.Resources.Default_Alert_New, _newSound);
        }
        
        ~RSSManager()
        {
            File.Delete(_alertSound.AbsolutePath);
            File.Delete(_newSound.AbsolutePath);
        }

        public void LoadSoundResource(byte[] source, Uri path)
        {
            MemoryStream soundFile = new MemoryStream(source);
            using (Stream output = new FileStream(path.AbsolutePath, FileMode.Create))
            {
                byte[] buffer = new byte[32 * 1024];
                int read;

                while ((read = soundFile.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, read);
                }
            }
        }

        public void PlayFlag()
        {
            if (AlertFlagSound == null)
            {
                //_wmp.URL = _newSound.AbsoluteUri;
                //_wmp.controls.play();
            }
        }
        public void PlayNew()
        {
            if (AlertFlagSound == null)
            {
                //_wmp.URL = _newSound.AbsoluteUri;
                //_wmp.controls.play();
            }
        }

        private string GetUriPlatform(E_Platforms platform)
        {
            string pp;

            if ((platform & E_Platforms.PS4) == E_Platforms.PS4 || (platform & E_Platforms.XB1) == E_Platforms.XB1)
                pp = Enum.GetName(typeof(E_Platforms), platform).ToLower() + ".";
            else
                pp = "";
            return _uriLeft + pp + _uriRight;
        }

        private async Task<bool> RefreshActivitiesList()
        {
            List<IActivity> tmp = new List<IActivity>();
            foreach (E_Platforms platform in Enum.GetValues(typeof(E_Platforms)))
            {
                if (platform != E_Platforms.None && (_platformsList & platform) == platform)
                {
                    tmp.AddRange(FeedToAlert(FeedToFeed(await GetFeed(GetUriPlatform(platform))), platform));
                    tmp.AddRange(MainActivitiesList.Where(i => i.Type == E_Activities.Alert && (i.Platform & platform) == platform && ((Alert)i).ExpirationDate.CompareTo(DateTime.UtcNow) > 0).ToList());
                }
            }
            MainActivitiesList = tmp.OrderBy(i => i.Platform).ThenBy(i => i.Type).ThenBy(i => i.PublishDate).ToList();
            return true;
        }

        private void RefreshCategories()
        {
            if (MainActivitiesList != null)
            { 
                ActivitiesList = new ObservableCollection<IActivity>(MainActivitiesList.Where(i => i.Done == false).ToList());
                AlertsList = new ObservableCollection<IActivity>(ActivitiesList.Where(i => i.Type == E_Activities.Alert).ToList());
                InvasionsList = new ObservableCollection<IActivity>(ActivitiesList.Where(i => i.Type == E_Activities.Invasion).ToList());
                OutbreaksList = new ObservableCollection<IActivity>(ActivitiesList.Where(i => i.Type == E_Activities.Outbreak).ToList());
                DoneList = new ObservableCollection<IActivity>(MainActivitiesList.Where(i => i.Done == true).ToList());
                NotifyPropertyChanged("Lists");
            }
        }

        public void Update()
        {
            RefreshActivitiesList();
        }

        public void MainListChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName == "MainActivitiesList")
                RefreshCategories();
        }

        public void ActivityStatusChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName == "Done")
            {
                RefreshCategories();
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Update();
            _timerElapses = DateTime.UtcNow.AddSeconds(_refreshRate);
        }

        void timer_Elapses(object sender, ElapsedEventArgs e)
        {
            NotifyPropertyChanged("TimerElapses");
        }

        public void MarkViewed()
        {
            foreach (IActivity a in MainActivitiesList)
            {
                a.Viewed = true;
            }
        }

        private async Task<SyndicationFeed> GetFeed(string uri)
        {
            XmlReader r = XmlReader.Create(uri);
            SyndicationFeed feed = SyndicationFeed.Load(r);
            r.Close();
            return feed;
        }

        public List<FeedDTO> FeedToFeed(SyndicationFeed feed)
        {
            List<FeedDTO> lst = new List<FeedDTO>();
            foreach (var item in feed.Items)
            {
                FeedDTO f = new FeedDTO();
                if (item.Id != null)
                    f.Id = item.Id;
                if (item.Title != null)
                    f.Title = item.Title.Text;
                if (item.Summary != null)
                    f.Description = item.Summary.Text;
                if (item.Authors != null && item.Authors.Count > 0)
                    f.Author = item.Authors[0].Email;
                if (item.PublishDate != null)
                    f.PublishDate = item.PublishDate;
                if (item.ElementExtensions.Where(p => p.OuterName == "faction").Count() > 0)
                    f.Faction = item.ElementExtensions.Where(p => p.OuterName == "faction").First().GetObject<XElement>().Value;
                if (item.ElementExtensions.Where(p => p.OuterName == "expiry").Count() > 0)
                    f.ExpireDate = DateTimeOffset.Parse(item.ElementExtensions.Where(p => p.OuterName == "expiry").First().GetObject<XElement>().Value);
                lst.Add(f);
            }
            return lst;
        }

        public List<IActivity> FeedToAlert(List<FeedDTO> feeds, E_Platforms platform)
        {
            bool newAlert = false;

            List<IActivity> lst = new List<IActivity>();
            foreach (FeedDTO f in feeds)
            {
                if (MainActivitiesList.Where(i => i.Type == E_Activities.Alert && i.Id == f.Id).Count() == 0)
                {
                    switch ((E_Activities)System.Enum.Parse(typeof(E_Activities), f.Author, true))
                    {
                        case E_Activities.Alert:
                            //Information to set similar to other activities
                            //It is separated because of the instantiation of different classes
                            newAlert = true;
                            Alert a = new Alert(f.Title);
                            a.Id = f.Id;
                            a.PublishDate = f.PublishDate;
                            a.Platform = platform;
                            a.PropertyChanged += ActivityStatusChanged; //This is used to ref

                            //Information to set different from other activities
                            a.Description = f.Description;
                            a.ExpirationDate = f.ExpireDate;
                            a.Faction = (E_Factions)System.Enum.Parse(typeof(E_Factions), f.Faction.Split('_')[1], true);

                            //Add the activity to the list
                            lst.Add(a);
                            break;
                        case E_Activities.Outbreak:
                            Outbreak o = new Outbreak(f.Title);
                            o.Id = f.Id;
                            o.PublishDate = f.PublishDate;
                            o.Platform = platform;
                            o.PropertyChanged += ActivityStatusChanged;

                            lst.Add(o);
                            break;
                        case E_Activities.Invasion:
                            Invasion i = new Invasion(f.Title);
                            i.Id = f.Id;
                            i.PublishDate = f.PublishDate;
                            i.Platform = platform;
                            i.PropertyChanged += ActivityStatusChanged;

                            lst.Add(i);
                            break;
                        default:
                            break;
                    }
                }
            }
            if (newAlert && !_firstUpdate)
                PlayNew();
            return lst;
        }
    }
}
