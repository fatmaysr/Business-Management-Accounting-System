using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class AdminLog
    {
        [Key]
        public int LogID { get; set; }
        public string Process { get; set; }
        public DateTime ProcessDate { get; set; }
        public string Info { get; set; }
    }
}