using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Common.Missions.Interfaces
{
    public interface IActivity
    {
        Enum Type { get; set; }

        String Id { get; set; }

        String Info { get; set; }

        DateTimeOffset PublishDate { get; set; }

        Boolean Done { get; set; }

        Boolean Viewed { get; set; }

        Boolean Marked { get; set; }

        Enum Platform { get; set; }
    }
}
