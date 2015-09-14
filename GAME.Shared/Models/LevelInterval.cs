using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Shared.Models
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
