using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Common.Core.Interfaces.Managers
{
    public interface IManagerDataGetter
    {
        Task<List<ExpandoObject>> GetExpando(string source);
    }
}
