using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; }
        public DateTime? Date { get; set; }

        // FK nullable veya cascade delete kapalı
        [ForeignKey("Customer")]
        public int CariID { get; set; }
        public virtual Customer Customer { get; set; }

        public string BankName { get; set; }
        public string Branch { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public string SecretToken { get; set; }
        public int Status { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}