using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class FastSaleProduct
    {
        [Key]
        public int ProductId { get; set; }
        public DateTime ? Date { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Value { get; set; }
        public int Status { get; set; }
    }

}
