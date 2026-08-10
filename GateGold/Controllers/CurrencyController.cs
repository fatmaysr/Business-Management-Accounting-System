using System;

using System.Web.Mvc;

using EntityLayer.Concrete;

using BusinessLayer.Abstract;

using BusinessLayer.Concrete;

using DataAccessLayer.EntityFramework;

using System.Linq;

using System.Collections.Generic;



namespace GateGold.Controllers

{

    public class CurrencyController : Controller

    {

        private readonly ICurrencyService _currencyService;

        private readonly ICurrencyDetailService _currencyDetailService;

        private readonly ICustomerService _customerService;



        public CurrencyController(
            ICurrencyService currencyService,
            ICurrencyDetailService currencyDetailService,
            ICustomerService customerService)
        {
            _currencyService = currencyService;
            _currencyDetailService = currencyDetailService;
            _customerService = customerService;
        }



        // ===================== CURRENCY CRUD =====================



        public ActionResult CurrencyBalanceList()

        {

            var currencies = _currencyService.GetList();

            return View(currencies);

        }





        [HttpGet]

        public ActionResult AddCurrency()

        {

            var customers = _customerService.GetList();

            ViewBag.Customers = customers ?? new List<Customer>();



            var currency = new Currency

            {

                Date = DateTime.Now

            };



            return View(currency);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddCurrency(Currency currency)

        {

            if (!ModelState.IsValid)

                return View(currency);



            // Eğer tarih set edilmemişse DateTime.Now ata

            if (currency.Date == default(DateTime))

                currency.Date = DateTime.Now;



            _currencyService.Add(currency);

            TempData["Success"] = "Döviz hesabı eklendi.";

            return RedirectToAction("CurrencyBalanceList");

        }





        [HttpGet]

        public ActionResult EditCurrency(int id)

        {

            var currency = _currencyService.GetById(id);

            if (currency == null)

                return HttpNotFound();



            return View(currency);

        }



        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult EditCurrency(Currency currency)

        {

            if (!ModelState.IsValid)

                return View(currency);



            _currencyService.Update(currency);

            TempData["Success"] = "Döviz hesabı güncellendi.";

            return RedirectToAction("CurrencyBalanceList");

        }



        public ActionResult DeleteCurrency(int id)

        {

            var currency = _currencyService.GetById(id);

            if (currency == null)

                return HttpNotFound();



            _currencyService.Delete(currency);

            TempData["Success"] = "Döviz hesabı silindi.";

            return RedirectToAction("CurrencyBalanceList");

        }



        // ===================== CURRENCY DETAIL CRUD =====================





        public ActionResult CurrencyDetailList(int currencyId)

        {

            var details = ((EfCurrencyDetailDal)_currencyDetailService).GetListWithCustomer(currencyId);

            ViewBag.Currency = _currencyService.GetById(currencyId);

            return View(details);

        }



        [HttpGet]

        public ActionResult AddCurrencyDetail(int? currencyId)

        {

            if (currencyId == null)

                return RedirectToAction("CurrencyBalanceList"); // veya uygun bir sayfaya yönlendir



            var customers = _customerService.GetList();

            ViewBag.Customers = customers ?? new List<Customer>();



            var detail = new CurrencyDetail

            {

                CariID = currencyId.Value,

                Date = DateTime.Now

            };



            return View(detail);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCurrencyDetail(CurrencyDetail detail)

        {

            if (!ModelState.IsValid)

                return View(detail);



            // Eğer tarih set edilmemişse DateTime.Now ata

            if (detail.Date == default(DateTime))

                detail.Date = DateTime.Now;



            _currencyDetailService.Add(detail);

            TempData["Success"] = "Döviz hareketi eklendi.";

            return RedirectToAction("CurrencyDetailList", new { currencyId = detail.CariID });

        }





        [HttpGet]

        public ActionResult EditCurrencyDetail(int id)

        {

            ViewBag.Customers = _customerService.GetList(); // business layer üzerinden



            var detail = _currencyDetailService.GetById(id);

            if (detail == null)

                return HttpNotFound();



            return View(detail);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditCurrencyDetail(CurrencyDetail detail)

        {

            if (!ModelState.IsValid)

                return View(detail);



            _currencyDetailService.Update(detail);

            TempData["Success"] = "Döviz hareketi güncellendi.";

            return RedirectToAction("CurrencyDetailList", new { currencyId = detail.CariID });

        }



        public ActionResult DeleteCurrencyDetail(int id)

        {

            var detail = _currencyDetailService.GetById(id);

            if (detail == null)

                return HttpNotFound();



            _currencyDetailService.Delete(detail);

            TempData["Success"] = "Döviz hareketi silindi.";

            return RedirectToAction("CurrencyDetailList", new { currencyId = detail.CariID });

        }

    }

}