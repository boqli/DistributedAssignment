using Common;
using DataAccess.Interfaces;
using Newtonsoft.Json;
using Presentation.Helper;
using Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Repositories
{
    public class APIDataAccess : IAPIDataAccess
    {
        MicroServices help = new MicroServices();
        public async Task<List<Symbols>> getSymbols()
        {
            List<Symbols> curr = new List<Symbols>();
            HttpClient client = help.APIs();
            HttpResponseMessage res = await client.GetAsync("api/API/getsymbols");
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                curr = JsonConvert.DeserializeObject<List<Symbols>>(senten);
            }
            return curr;
        }
    }
}
