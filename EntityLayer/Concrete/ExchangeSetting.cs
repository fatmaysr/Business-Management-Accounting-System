using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ExchangeSetting
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Customer { get; set; }
        public string Unit { get; set; }
        public int Provider { get; set; }
        public int Type { get; set; }
        public string SecretToken { get; set; }
        public int Status { get; set; }
    }

}
