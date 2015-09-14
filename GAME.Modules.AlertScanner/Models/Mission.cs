using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Modules.Warframe.AlertScanner.Models
{
    public class Mission
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
