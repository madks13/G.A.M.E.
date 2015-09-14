using System;


namespace GAME.Shared.Models
{
    public enum PlaceType
    {
        Assassinate,
        Defense,
        MobileDefense,
        Interception,
        Raid,
        Suvival,
        Hive,
        Exterminate,
        Sabotage
    }
    public class Place
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PlaceType Type { get; set; }

        public LevelInterval Level { get; set; }
    }
}
