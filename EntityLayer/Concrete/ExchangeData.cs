using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ExchangeData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public string Eu { get; set; }
        public string Usd { get; set; }
        public string Aud { get; set; }
        public string Dkk { get; set; }
        public string Gbp { get; set; }
        public string Chf { get; set; }
        public string Sek { get; set; }
        public string Cad { get; set; }
        public string Kwd { get; set; }
        public string Nok { get; set; }
        public string Sar { get; set; }
        public string Jpy { get; set; }
        public string GramGold { get; set; }
        public string QuarterGold { get; set; }
        public string HalfGold { get; set; }
        public string FullGold { get; set; }
        public string Trappings { get; set; }
        public string SecretToken { get; set; }
        public int Status { get; set; }
    }

}
