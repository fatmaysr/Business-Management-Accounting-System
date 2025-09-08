using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Type
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public string Value { get; set; }
        public string WorkmanValue { get; set; }
        public string SecretToken { get; set; }
        public int Status { get; set; }


    }
}

