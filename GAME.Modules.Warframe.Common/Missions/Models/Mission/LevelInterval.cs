namespace GAME.Modules.Warframe.Common.Missions.Models.Mission
{
    public class LevelInterval
    {
        public int Min { get; set; }

        public int Max { get; set; }

        public override string ToString()
        {
            return "[" + Min + " - " + Max + "]";
        }
    }
}
