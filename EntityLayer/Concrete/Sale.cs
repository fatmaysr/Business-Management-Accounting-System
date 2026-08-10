using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual FastSaleProduct Product { get; set; }

        // Satış anındaki ürün adı ve fiyatı burada da tutulur (ürün sonradan
        // değişse/silinse bile geçmiş satış kaydı bozulmasın diye).
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public string PaymentType { get; set; } // "Nakit" / "Kart"

        [ForeignKey("CashCase")]
        public int CaseId { get; set; }
        public virtual CashCase CashCase { get; set; }

        [ForeignKey("Customer")]
        public int CariID { get; set; }
        public virtual Customer Customer { get; set; }

        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}