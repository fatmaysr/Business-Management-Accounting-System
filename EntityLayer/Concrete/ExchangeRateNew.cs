using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ExchangeRateNew
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
        public string Provider { get; set; }
        public string SecretToken { get; set; }
        public int Status { get; set; }
    }

}
