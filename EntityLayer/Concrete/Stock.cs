using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Stock
    {

        [Key]
        public int StockId { get; set; }          // Primary Key
        public string StockName { get; set; }     // Stok Adı
        public int Quantity { get; set; }         // Miktar
        public decimal Price { get; set; }        // Birim fiyat
        public DateTime CreatedDate { get; set; } 
    }
}
