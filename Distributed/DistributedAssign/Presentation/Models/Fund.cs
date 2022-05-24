using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Fund
    {

        public int IBAN { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public int fundNo { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public double OpeningBalance { get; set; }
        public string currencyCode { get; set; }
        public string Payee { get; set; }
        public string id { get; set; }
        public bool isActive { get; set; }
        
    }
}
