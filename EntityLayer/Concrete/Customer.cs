using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EntityLayer.Concrete
{
    public class Customer
    {
        [Key]
        public int CariID { get; set; }

        [Required(ErrorMessage = "Cari Kodu zorunludur.")]
        [StringLength(50)]
        public string CariKodu { get; set; }

        [Required(ErrorMessage = "Cari Adı zorunludur.")]
        public string CariAdi { get; set; }

        public string CariTipi { get; set; }

        [StringLength(50)]
        public string VergiDairesi { get; set; }

        public string VergiNo { get; set; }

        public string TCKimlikNo { get; set; }

        public string Adres { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon giriniz.")]
        public string Telefon { get; set; }

        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        public string Email { get; set; }

        public string WebSitesi { get; set; }


        public virtual ICollection<Bank> Banks { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}