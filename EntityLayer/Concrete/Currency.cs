using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Currency
    {

        [Key]
        public int CurrencyId { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Customer")]
        public int CariID { get; set; }
        public virtual Customer Customer { get; set; }  
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TaxNumber { get; set; }
        public string Iban { get; set; }
        public string TaxAdmin { get; set; } 
        public int? Status { get; set; }
    }

}
