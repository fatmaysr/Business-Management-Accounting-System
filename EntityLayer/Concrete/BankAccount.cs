using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankAccountId { get; set; }
        public DateTime? Date { get; set; }

        [ForeignKey("Customer")]
        public int CariID { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("Bank")]
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }

        public string ClassName { get; set; }
        public string ClassNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Iban { get; set; }
        public string AccountName { get; set; }
        public int PosStatus { get; set; }
        public int CcStatus { get; set; }
        public string SecretToken { get; set; }
        public int Status { get; set; }
    }
}