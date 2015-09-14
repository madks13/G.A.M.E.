using System;
using GAME.Shared.Interfaces;

namespace GAME.Shared.Models.Rewards
{
    public class RewardCredits : IReward
    {
        public E_Rewards RewardType { get; set; }

        public E_RewardGivers RewardGiver { get; set; }

        public uint Amount { get; set; }

        public string Object
        {
            get { return "cr"; }
        }

        private void Parse()
        {
            if (_info.Contains("K"))
            {
                Amount = uint.Parse(_info.Substring(0, _info.Length - 1));
                Amount *= 1000;
            }
            else if (_info.Contains("cr"))
                Amount = uint.Parse(_info.Substring(0, _info.Length - 2));
            else
                Amount = uint.Parse(_info);
        }

        public RewardCredits(string info)
        {
            RewardType = E_Rewards.Credits;
            _info = info;
            Parse();
        }

        private string _info;
        public string Info
        {
            get 
            {
                return Amount + "cr " + (RewardGiver != E_RewardGivers.Lotus ? RewardGiver.ToString() : "");
            }
            set
            {
                _info = value;
                Parse();
            }
        }

        public string Type
        { 
            get {return RewardType.ToString();} 
            set {RewardType = (E_Rewards)Enum.Parse(typeof(E_Rewards), value);} 
        }
    }
}
