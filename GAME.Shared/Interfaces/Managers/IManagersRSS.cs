using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using GAME.Shared.Models;

namespace GAME.Shared.Interfaces.Managers
{
    interface IManagersRSS
    {
        Task<List<FeedDTO>> GetFeeds(string source);
    }
}
