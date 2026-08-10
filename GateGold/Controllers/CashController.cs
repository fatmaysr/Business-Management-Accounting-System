using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace GateGold.Controllers
{
    public class CashController : Controller
    {
        private readonly ICashCaseService _cashCaseService;
        private readonly ICaseTransactionService _cashTransactionService;
        private readonly ICustomerService _customerService;

        public CashController(
            ICashCaseService cashCaseService,
            ICaseTransactionService cashTransactionService,
            ICustomerService customerService)
        {
            _cashCaseService = cashCaseService;
            _cashTransactionService = cashTransactionService;
            _customerService = customerService;
        }

        // ===== CASH CASE CRUD =====

        public ActionResult CashList()
        {
            var cashCases = _cashCaseService.GetList();
            return View(cashCases);
        }

        [HttpGet]
        public ActionResult AddCash()
        {
            ViewBag.Customers = new SelectList(_customerService.GetList(), "CariID", "CariAdi");
            return View(new CashCase());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCash(CashCase cashCase)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = new SelectList(_customerService.GetList(), "CariID", "CariAdi", cashCase.CariID);
                return View(cashCase);
            }

            _cashCaseService.Add(cashCase);
            TempData["Success"] = "Kasa başarıyla eklendi.";
            return RedirectToAction("CashList");
        }

        [HttpGet]
        public ActionResult EditCash(int id)
        {
            var cashCase = _cashCaseService.GetById(id);
            if (cashCase == null)
                return HttpNotFound();

            ViewBag.Customers = new SelectList(_customerService.GetList(), "CariID", "CariAdi", cashCase.CariID);
            return View(cashCase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCash(CashCase cashCase)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = new SelectList(_customerService.GetList(), "CariID", "CariAdi", cashCase.CariID);
                return View(cashCase);
            }

            _cashCaseService.Update(cashCase);
            TempData["Success"] = "Kasa bilgileri güncellendi.";
            return RedirectToAction("CashList");
        }

        public ActionResult DeleteCash(int id)
        {
            var cashCase = _cashCaseService.GetById(id);
            if (cashCase == null)
                return HttpNotFound();

            _cashCaseService.Delete(cashCase);
            TempData["Success"] = "Kasa silindi.";
            return RedirectToAction("CashList");
        }

        // ===== CASH TRANSACTION CRUD =====

        public ActionResult CashTransactions(int? CariID, string currency, DateTime? date)
        {
            ViewBag.Customers = _customerService.GetList()
                .Select(c => new SelectListItem
                {
                    Text = c.CariAdi,
                    Value = c.CariID.ToString()
                }).ToList();

            var transactions = _cashTransactionService.GetList();

            if (CariID.HasValue)
                transactions = transactions.Where(x => x.CariID == CariID.Value).ToList();

            if (!string.IsNullOrEmpty(currency))
                transactions = transactions.Where(x => x.Currency == currency).ToList();

            if (date.HasValue)
                transactions = transactions.Where(x => x.Date.Date == date.Value.Date).ToList();

            return View(transactions);
        }

        [HttpGet]
        public ActionResult AddCashTransaction()
        {
            ViewBag.CashCases = new SelectList(_cashCaseService.GetList(), "CaseId", "CaseName");
            ViewBag.Customers = new SelectList(_customerService.GetList(), "CariID", "CariAdi");
            return View(new CaseTransaction());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCashTransaction(CaseTransaction transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CashCases = new SelectList(_cashCaseService.GetList(), "CaseId", "CaseName", transaction.CaseId);
                ViewBag.Customers = new SelectList(_customerService.GetList(), "CariID", "CariAdi", transaction.CariID);
                return View(transaction);
            }

            _cashTransactionService.Add(transaction);
            TempData["Success"] = "Kasa hareketi eklendi.";
            return RedirectToAction("CashTransactions");
        }

        [HttpGet]
        public ActionResult EditCashTransaction(int id)
        {
            var transaction = _cashTransactionService.GetById(id);
            if (transaction == null)
                return HttpNotFound();

            ViewBag.CashCases = new SelectList(_cashCaseService.GetList(), "CaseId", "CaseName", transaction.CaseId);
            ViewBag.Customers = new SelectList(_customerService.GetList(), "CariID", "CariAdi", transaction.CariID);

            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCashTransaction(CaseTransaction transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CashCases = new SelectList(_cashCaseService.GetList(), "CaseId", "CaseName", transaction.CaseId);
                ViewBag.Customers = new SelectList(_customerService.GetList(), "CariID", "CariAdi", transaction.CariID);
                return View(transaction);
            }

            _cashTransactionService.Update(transaction);
            TempData["Success"] = "Kasa hareketi güncellendi.";
            return RedirectToAction("CashTransactions");
        }

        public ActionResult DeleteCashTransaction(int id)
        {
            var transaction = _cashTransactionService.GetById(id);
            if (transaction == null)
                return HttpNotFound();

            _cashTransactionService.Delete(transaction);
            TempData["Success"] = "Kasa hareketi silindi.";
            return RedirectToAction("CashTransactions");
        }

        // ===== DAILY CASH (Sadece liste/rapor) =====
     
        public ActionResult DailyCash(DateTime? selectedDate)
        {
            var transactions = _cashTransactionService.GetList();

            if (selectedDate.HasValue)
            {
                transactions = transactions
                    .Where(t => t.Date.Date == selectedDate.Value.Date)
                    .ToList();
            }

            return View(transactions);
        }
    }
}