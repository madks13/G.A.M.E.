using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GAME.Shared.Models;

namespace GAME.Shared.Interfaces
{
    public enum E_Activities
    {
        Unknown,
        Alert,
        Invasion,
        Outbreak
    }

    public enum E_Platforms
    {
        None = 0x00,
        PC = 0x01,
        PS4 = 0x02,
        XB1 = 0x04
    }

    public interface IActivity
    {
        E_Activities Type { get; set; }

        string Id { get; set; }

        string Info { get; set; }

        DateTimeOffset PublishDate { get; set; }

        bool Done { get; set; }

        bool Viewed { get; set; }

        E_Platforms Platform { get; set; }

        Mission Mission { get; set; }
    }
}
