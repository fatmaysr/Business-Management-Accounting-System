using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CaseTransaction
    {

        [Key]
        public int CaseTransactionId { get; set; } // KasaHareketID

        [ForeignKey("CashCase")]
        public int CaseId { get; set; } // KasaID
        public virtual CashCase CashCase { get; set; }

        public DateTime Date { get; set; } // HareketTarihi
        public string Type { get; set; } // HareketTipi
        public decimal Amount { get; set; } // Tutar
        public string Currency { get; set; } // DovizTipi
        public decimal ExchangeRate { get; set; } // DovizKuru

        [ForeignKey("Customer")]
        public int CariID { get; set; } // CariID
        public virtual Customer Customer { get; set; }

        public string Description { get; set; } // Aciklama
        public int UserId { get; set; } // KullaniciID
    }
}