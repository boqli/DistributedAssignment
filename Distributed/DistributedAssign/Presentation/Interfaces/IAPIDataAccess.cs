using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Interfaces
{
    public interface IAPIDataAccess
    {

        Task<List<Symbols>> getSymbols();
    }
}
