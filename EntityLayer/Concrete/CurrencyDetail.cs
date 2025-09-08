using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CurrencyDetail
    {

        [Key]
        public int CurrecyDetailId { get; set; }


        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Customer")]
        public int CariID { get; set; }
        public virtual Customer Customer { get; set; } 
        public int? Type { get; set; }
        public int? Amount { get; set; }
        public decimal ExchangeRate { get; set; }       // Kur
        public string Unit { get; set; }
        public string Total { get; set; }
        public string Description { get; set; }        
        public int? Status { get; set; }
  
    }

}
