using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class FastSaleCategory
    {

        [Key]
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
 
        public int Status { get; set; }
    }

}
