using System.Collections.Generic;
using EntityLayer.Concrete;
using BusinessLayer.Concrete; // TcmbRateDto için eklendi

namespace Nexus.Models
{
    public class DashboardViewModel
    {
        public int AktifKasaSayisi { get; set; }
        public int CariSayisi { get; set; }
        public int KullaniciSayisi { get; set; }
        public int BugunkuHareketSayisi { get; set; }

        public decimal ToplamGiris { get; set; }
        public decimal ToplamCikis { get; set; }
        public List<CaseTransaction> SonHareketler { get; set; } = new List<CaseTransaction>();

        // Canlı Kurlar İçin Eklendi:
        public List<TcmbRateDto> LiveRates { get; set; } = new List<TcmbRateDto>();
    }
}