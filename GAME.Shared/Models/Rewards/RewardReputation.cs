using System;
using GAME.Shared.Interfaces;
using GAME.Shared.Models.Groups;

namespace GAME.Shared.Models.Rewards
{
    public class RewardReputation : IReward
    {

        public E_Rewards RewardType {get; set;}

        public E_RewardGivers RewardGiver { get; set; }
        
        public uint Amount { get; set; }

        public E_Syndicates Group { get; set; }

        private void Parse()
        {
        }

        public RewardReputation(string info)
        {
            RewardType = E_Rewards.Reputation;
            RewardGiver = E_RewardGivers.Syndicate;
            _info = info;
            Parse();
        }

        private string _info;
        public string Info
        {
            get
            {
                return "Reward type : " + Type + ", Reward amount : " + Amount + ", Syndicate : " + Group.ToString();
            }
            set
            {
                _info = value;
                Parse();
            }
        }

        public string Type { get; set; }
    }
}
