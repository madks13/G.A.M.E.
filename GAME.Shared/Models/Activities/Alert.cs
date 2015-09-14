using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;

using GAME.Shared.Common;
using GAME.Shared.Models.Groups;
using GAME.Shared.Models.Rewards;
using GAME.Shared.Interfaces;

namespace GAME.Shared.Models.Activities
{

    public class Alert : PropertiesObserver, IActivity
    {
        #region Common Resources

        private E_Activities _type;
        
        public E_Activities Type
        {
            get { return _type; } 
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }

        private string _id;

        public string Id
        {
            get { return _id; } 
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
        
        private string _info;

        public string Info
        { 
            get
            {
                string r = "";

                foreach (IReward reward in Rewards)
                {
                    r += reward.Info + " - ";
                }
                r += Mission.Info + " - " + TimeLeft;
                return r;
            } 
            set
            {
                _info = value;
                Parse();
            }
        }

        private DateTimeOffset _publishDate;

        public DateTimeOffset PublishDate
        {
            get { return _publishDate; } 
            set
            {
                _publishDate = value;
                NotifyPropertyChanged("PublishDate");
            }
        }

        private bool _done;

        public bool Done
        {
            get { return _done; }
            set
            {
                _done = value;
                NotifyPropertyChanged("Done");
            }
        }

        private bool _viewed;
       
        public bool Viewed
        {
            get { return _viewed; }
            set
            {
                _viewed = value;
                NotifyPropertyChanged("Viewed");
            }
        }

        private E_Platforms _platform;

        public E_Platforms Platform
        {
            get { return _platform; }
            set
            {
                _platform = value;
                NotifyPropertyChanged("Platform");
            }
        }
 
        private List<IReward> _rewards;
        public List<IReward> Rewards
        {
            get { return _rewards; }
            set
            {
                _rewards = value;
                NotifyPropertyChanged("Rewards");
            }
        }

        private Mission _mission;

        public Mission Mission
        {
            get { return _mission; }
            set
            {
                _mission = value;
                NotifyPropertyChanged("Mission");
            }
        }

        #endregion
        
        #region Special Resources

        Timer _timer;
        
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        private E_Factions _faction;

        public E_Factions Faction
        {
            get { return _faction; }
            set
            {
                _faction = value;
                NotifyPropertyChanged("Faction");
            }
        }

        private DateTimeOffset _expirationDate;

        public DateTimeOffset ExpirationDate
        {
            get { return _expirationDate; }
            set
            {
                _expirationDate = value;
                NotifyPropertyChanged("ExpirationDate");
                NotifyPropertyChanged("TimeLeft");
            }
        }

        public string TimeLeft
        {
            get
            {
                if (ExpirationDate.CompareTo(DateTime.UtcNow) > 0)
                {
                    TimeSpan tl = ExpirationDate - DateTime.UtcNow;
                    return (tl.Days != 0 ? tl.Days + "d " : "") + (tl.Hours != 0 ? tl.Hours + "h " : "") + (tl.Minutes != 0 ? tl.Minutes + "m " : "") + tl.Seconds + "s";
                }
                else
                    return "0s";
            }
        }

        #endregion
        
        #region Events

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.NotifyPropertyChanged("TimeLeft");
            this.NotifyPropertyChanged("Info");
        }
        
        #endregion
        
        private void Parse()
        {
            string[] parts = _info.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim();
                int n;
                if (i == 0 && (!parts[i].Contains("cr") || !int.TryParse(parts[i].Substring(0, parts[i].Length - 2), out n)))
                {
                    RewardObject o = new RewardObject(parts[i]);
                    o.RewardGiver = E_RewardGivers.Lotus;
                    Rewards.Add(o);
                }
                if (parts[i].Contains("cr"))
                {
                    RewardCredits c = new RewardCredits(parts[i]);
                    c.RewardGiver = E_RewardGivers.Lotus;
                    Rewards.Add(c);
                }
                if (i != 0 && parts[i].Contains("("))
                {
                    Mission = new Mission(parts[i]);
                }
            }
        }

        public Alert(string info)
        {
            _rewards = new List<IReward>();
            _type = E_Activities.Alert;
            _done = false;
            _viewed = false;
            _info = info;

            Parse();

            _timer = new Timer(1000);
            _timer.Elapsed += timer_Elapsed;
            _timer.Start();
        }
    }
}
