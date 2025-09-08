using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CashCase
    {

        [Key]
        public int CaseId { get; set; }

        [ForeignKey("Customer")]
        public int CariID { get; set; }    // Müşteri/ilgili ID
        public virtual Customer Customer { get; set; }
        public string CaseName { get; set; } // Kasa adı
        public string CaseCode { get; set; } // Kasa kodu
        public string Currency { get; set; } // Para birimi
        public int Status { get; set; } // 1=Aktif, 0=Pasif
    }
}