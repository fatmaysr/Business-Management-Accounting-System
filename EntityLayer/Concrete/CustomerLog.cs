using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CustomerLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string IpAddress { get; set; }
        public string Browser { get; set; }
        public string SecretToken { get; set; }
        public int Status { get; set; }
    }
}
