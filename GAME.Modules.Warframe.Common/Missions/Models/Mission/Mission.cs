using System;

namespace GAME.Modules.Warframe.Common.Missions.Models.Mission
{
    class Mission
    {
        public string Info { get; set; }

        public string Planet { get; set; }

        public string Place { get; set; }

        private void Parse()
        {
            string[] parts = Info.Split("()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Place = parts[0].Trim();
            Planet = parts[1].Trim();
        }

        public Mission(string info)
        {
            Info = info;
            Parse();
        }
    }
}
