using System;
using GAME.Shared.Interfaces;

namespace GAME.Shared.Models.Rewards
{
    public class RewardObject : IReward
    {
        public E_Rewards RewardType { get; set; }

        public E_RewardGivers RewardGiver { get; set; }
        
        public uint Amount { get; set; }

        public string Object { get; set; }

        private void Parse()
        {
            if (_info.Contains("("))
            {
                string[] parts = _info.Split("()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Object = parts[0].Trim();
                Type = parts[1].Trim();
                Amount = 1;
            }
            else if (_info.Contains("x"))// && !_info.Contains("("))
            {
                Type = "Resource";
                string[] parts = _info.Split("x".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Amount = uint.Parse(parts[0].Trim());
                Object = parts[1].Trim();
            }
            else
            {
                Amount = 1;
                Type = "Item";
                Object = _info;
            }
        }

        public string Type { get; set; }

        public RewardObject(string info)
        {
            RewardType = E_Rewards.Object;
            _info = info;
            Parse();
        }

        private string _info;
        public string Info
        {
            get
            {
                string r = "";
                if (RewardGiver != E_RewardGivers.Lotus)
                {
                    r += RewardGiver.ToString() + " (" + Amount + "x " + Object + ")";
                }
                else
                {
                    r += Object + " (" + Type + ")";
                }
                return r;
            }
            set
            {
                _info = value;
                Parse();
            }
        }
    }
}
