using System.Collections.Generic;
using EntityLayer.Concrete;

namespace GateGold.Models
{
    public class DashboardViewModel
    {
        public int AktifKasaSayisi { get; set; }
        public int CariSayisi { get; set; }
        public int KullaniciSayisi { get; set; }
        public int BugunkuHareketSayisi { get; set; }
        public List<CaseTransaction> SonHareketler { get; set; } = new List<CaseTransaction>();
    }
}