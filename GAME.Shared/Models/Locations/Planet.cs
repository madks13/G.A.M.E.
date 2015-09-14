using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Shared.Models
{
    public enum Planets
    {
        None,
        Ceres,
        Earth,
        Eris,
        Europa,
        Jupiter,
        Mars,
        Mercury,
        Neptune,
        Phobos,
        Pluto,
        Saturn,
        Sedna,
        Uranus,
        Venus,
        Void
    }
    public class Planet
    {
        public static Planets GetPlanet(string planet)
        {
            return (Planets)System.Enum.Parse(typeof(Planets), planet, true);
        }
        public int Id { get; set; }

        public string Name { get; set; }

        List<Place> Places { get; set; }

        public LevelInterval Level { get; set; }
    }
}
