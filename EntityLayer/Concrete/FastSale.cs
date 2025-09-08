using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class FastSale
    {

        [Key]
        public int FastSaleId { get; set; }
        public DateTime Date { get; set; }
        public int CariID { get; set; }
        public decimal Total { get; set; }
        public string FastToken { get; set; }
        public string Type { get; set; }
       public int Status { get; set; }
    }

}
