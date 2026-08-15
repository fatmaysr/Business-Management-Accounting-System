using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Abstract;

namespace Nexus.Controllers
{
    public class ReportingController : Controller
    {
        private readonly ICaseTransactionService _transactionService;
        private readonly ICustomerService _customerService;

        public ReportingController(
            ICaseTransactionService transactionService,
            ICustomerService customerService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
        }

        // Para birimine göre toplam giriş/çıkış özeti
        public ActionResult CashStatus()
        {
            var summary = _transactionService.GetList()
                .GroupBy(t => t.Currency ?? "Belirtilmemiş")
                .Select(g => new CashStatusRow
                {
                    Currency = g.Key,
                    TotalIn = g.Where(x => x.Type == "Giriş").Sum(x => x.Amount),
                    TotalOut = g.Where(x => x.Type == "Çıkış").Sum(x => x.Amount)
                })
                .ToList();

            return View(summary);
        }

        // Cari bazlı hareket ekstresi (filtrelenebilir)
        public ActionResult AccountMovements(int? cariId, DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Customers = _customerService.GetList();
            ViewBag.SelectedCariId = cariId;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            var transactions = _transactionService.GetList().AsEnumerable();

            if (cariId.HasValue)
                transactions = transactions.Where(t => t.CariID == cariId.Value);

            if (startDate.HasValue)
                transactions = transactions.Where(t => t.Date.Date >= startDate.Value.Date);

            if (endDate.HasValue)
                transactions = transactions.Where(t => t.Date.Date <= endDate.Value.Date);

            var list = transactions.OrderByDescending(t => t.Date).ToList();

            ViewBag.ToplamGiris = list.Where(x => x.Type == "Giriş").Sum(x => x.Amount);
            ViewBag.ToplamCikis = list.Where(x => x.Type == "Çıkış").Sum(x => x.Amount);

            return View(list);
        }
    }

    public class CashStatusRow
    {
        public string Currency { get; set; }
        public decimal TotalIn { get; set; }
        public decimal TotalOut { get; set; }
        public decimal Balance => TotalIn - TotalOut;
    }
}