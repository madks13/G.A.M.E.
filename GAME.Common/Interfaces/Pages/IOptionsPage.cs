using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAME.Common.Core.Models;

namespace GAME.Common.Core.Interfaces.Pages
{
    public interface IOptionsPage
    {
        event EventHandler<ClosureEventArgs> OptionsClosed;

        void RaiseOptionsClosed(ClosureEventArgs e);
    }
}
