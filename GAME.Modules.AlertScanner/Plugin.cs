using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;

using GAME.Common.Plugin;
using GAME.Common.Core.Interfaces;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace GAME.Modules.Warframe.AlertScanner
{
    class Plugin : CorePlugin
    {
        public Plugin()
        {
            _moduleType = typeof(Module);
        }

        public override string Type
        {
            get { return @"Game"; }
        }

        public override string SubType
        {
            get { return @"Warframe"; }
        }

        public override string Name
        {
            get { return @"Alert Scanner"; }
        }
    }
}
