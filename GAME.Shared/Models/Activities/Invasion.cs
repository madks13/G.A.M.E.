using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using GAME.Shared.Common;
using GAME.Shared.Interfaces;
using GAME.Shared.Models.Groups;
using GAME.Shared.Models.Rewards;

namespace GAME.Shared.Models.Activities
{
    public class Invasion : PropertiesObserver, IActivity
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
            get { return _info; }
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

        private List<E_Factions> _factions;
        public List<E_Factions> Factions
        {
            get { return _factions; }
            set
            {
                _factions = value;
                NotifyPropertyChanged("Factions");
            }
        }

        #endregion

        private void Parse()
        {
            string pattern = @"^\d+[K,cr]$";
            Regex rgx = new Regex(pattern);
            string[] parts = _info.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Mission = new Models.Mission(parts[1]);
            parts = parts[0].Split("VS.".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string side in parts)
            {
                string[] ri = side.Trim().Split("()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                MatchCollection matches = rgx.Matches(ri[1]);
                if (matches.Count > 0)//ri[1].Contains("x "))
                {
                    RewardCredits r = new RewardCredits(ri[1]);
                    r.RewardGiver = (E_RewardGivers)Enum.Parse(typeof(E_RewardGivers), ri[0]);
                    Rewards.Add(r);
                }
                else
                {
                    RewardObject r = new RewardObject(ri[1]);
                    r.RewardGiver = (E_RewardGivers)Enum.Parse(typeof(E_RewardGivers), ri[0]);
                    Rewards.Add(r);
                }
            }

        }

        public Invasion(string info)
        {
            _rewards = new List<IReward>();
            _type = E_Activities.Invasion;
            _done = false;
            _viewed = false;
            _info = info;

            Parse();
        }
    }

}
