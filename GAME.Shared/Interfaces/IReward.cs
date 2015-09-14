using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Shared.Interfaces
{
    public enum E_Rewards
    {
        None,
        Credits,
        Object,
        Reputation
    }

    public enum E_RewardGivers
    {
        None,
        Lotus,
        Grineer,
        Corpus,
        Syndicate
    }

    public interface IReward
    {
        E_Rewards RewardType { get; set; }

        E_RewardGivers RewardGiver { get; set; }

        string Type { get; set; }

        string Info { get; set; }
    }
}
