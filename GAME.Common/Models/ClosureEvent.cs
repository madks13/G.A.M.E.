using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Common.Core.Models
{
    public enum CloseReason
    {
        Unknown,
        Validation,
        Cancellation
    }

    public class ClosureEventArgs : EventArgs
    {
        public CloseReason Reason { get; set; }

        public ClosureEventArgs(CloseReason reason)
        {
            Reason = reason;
        }
    }
}
