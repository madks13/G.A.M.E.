using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Common.Plugin
{
    public class Sponsor : MarshalByRefObject, ISponsor
    {

        public Boolean Release { get; set; }

        public TimeSpan Renewal(ILease lease)
        {
            if (lease == null || lease.CurrentState != LeaseState.Renewing || Release)
                return TimeSpan.Zero; // don't renew
            return TimeSpan.FromMinutes(1); // renew for a second, or however long u want
        }
    }
}
