using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class BankTransaction
    {

        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Bank")]
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
        public string CurrentToken { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("BankAccount")]
        public int BankAccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public int Cuurency {  get; set; }
        public string Price{ get; set; }
        public int Type{ get; set; }
        public string Content{ get; set; }
        public int Method { get; set; }
        public string SecretToken { get; set; }
        public int Status { get; set; }


    }
}
