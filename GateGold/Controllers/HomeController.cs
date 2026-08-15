using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using Nexus.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Nexus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICashCaseService _cashCaseService;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly ICaseTransactionService _caseTransactionService;

        public HomeController(
            ICashCaseService cashCaseService,
            ICustomerService customerService,
            IUserService userService,
            ICaseTransactionService caseTransactionService)
        {
            _cashCaseService = cashCaseService;
            _customerService = customerService;
            _userService = userService;
            _caseTransactionService = caseTransactionService;
        }


        public ActionResult Index()
        {
            var tumHareketler = _caseTransactionService.GetList();
            var tcmbService = new TcmbCurrencyService();
            var canliKurlar = tcmbService.GetTodayRates();

            // GERÇEK VERİLER: Kasa hareketlerinden toplam girişi ve çıkışı hesaplıyoruz
            decimal toplamGiris = tumHareketler.Where(x => x.Type == "Giriş").Sum(x => x.Amount);
            decimal toplamCikis = tumHareketler.Where(x => x.Type == "Çıkış").Sum(x => x.Amount);

            var model = new DashboardViewModel
            {
                AktifKasaSayisi = _cashCaseService.GetList().Count(x => x.Status == 1),
                CariSayisi = _customerService.GetList().Count,
                KullaniciSayisi = _userService.GetList().Count,
                BugunkuHareketSayisi = tumHareketler.Count(x => x.Date.Date == DateTime.Today),
                SonHareketler = tumHareketler.OrderByDescending(x => x.Date).Take(5).ToList(),
                LiveRates = canliKurlar,

                // Yeni eklediğimiz özellikler (DashboardViewModel modeline bunları eklemen gerekebilir)
                ToplamGiris = toplamGiris,
                ToplamCikis = toplamCikis
            };

            return View(model);
        }
    }
}