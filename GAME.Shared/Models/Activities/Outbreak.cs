using System;
using System.Collections.Generic;

using GAME.Shared.Common;
using GAME.Shared.Interfaces;
using GAME.Shared.Models.Groups;
using GAME.Shared.Models.Rewards;

namespace GAME.Shared.Models.Activities
{
    public class Outbreak : PropertiesObserver, IActivity
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

        #endregion

        private void Parse()
        {
            string[] parts = _info.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (parts[0].Contains("x"))
            {
                RewardObject o = new RewardObject(parts[0].Trim());
                o.RewardGiver = E_RewardGivers.Lotus;
                Rewards.Add(o);
            }
            else
            {
                RewardCredits c = new RewardCredits(parts[0].Trim());
                c.RewardGiver = E_RewardGivers.Lotus;
                Rewards.Add(c);
            }
            if (parts[1].Contains("PHORID SPAWN"))
            {
                parts[1] = parts[1].Replace("PHORID SPAWN", "");
                Description = "Phorid Spawn";
            }
            else
                Description = "Outbreak";
            Mission = new Mission(parts[1].Trim());
        }

        public Outbreak(string info)
        {
            _rewards = new List<IReward>();
            _type = E_Activities.Outbreak;
            _faction = E_Factions.Infestation;
            _done = false;
            _viewed = false;
            _info = info;

            Parse();
        }
    }
}
