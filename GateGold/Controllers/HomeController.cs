using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using GateGold.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GateGold.Controllers
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

            // Canlı kurları TCMB'den çekiyoruz
            var tcmbService = new TcmbCurrencyService();
            var canliKurlar = tcmbService.GetTodayRates();

            var model = new DashboardViewModel
            {
                AktifKasaSayisi = _cashCaseService.GetList().Count(x => x.Status == 1),
                CariSayisi = _customerService.GetList().Count,
                KullaniciSayisi = _userService.GetList().Count,
                BugunkuHareketSayisi = tumHareketler.Count(x => x.Date.Date == DateTime.Today),
                SonHareketler = tumHareketler.OrderByDescending(x => x.Date).Take(5).ToList(),
                LiveRates = canliKurlar // Modele ekledik
            };

            return View(model);
        }
    }
}