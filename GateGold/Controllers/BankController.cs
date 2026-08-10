using System;
using System.Linq;
using System.Web.Mvc;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace GateGold.Controllers
{
    [Authorize]
    public class BankController : Controller
    {
        private readonly IBankService _bankService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IBankTransactionService _transactionService;

        public BankController(
            IBankService bankService,
            IBankAccountService bankAccountService,
            IBankTransactionService transactionService)
        {
            _bankService = bankService;
            _bankAccountService = bankAccountService;
            _transactionService = transactionService;
        }

        // ===== BANK CRUD =====

        public ActionResult BankList()
        {
            var banks = _bankService.GetList();
            return View(banks);
        }

        [HttpGet]
        public ActionResult AddBank()
        {
            return View(new Bank());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBank(Bank bank)
        {
            if (!ModelState.IsValid)
                return View(bank);

            _bankService.Add(bank);
            TempData["Success"] = "Banka başarıyla eklendi.";
            return RedirectToAction("BankList");
        }

        [HttpGet]
        public ActionResult EditBank(int id)
        {
            var bank = _bankService.GetById(id);
            if (bank == null)
                return HttpNotFound();

            return View(bank);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBank(Bank bank)
        {
            if (!ModelState.IsValid)
                return View(bank);

            _bankService.Update(bank);
            TempData["Success"] = "Banka bilgileri güncellendi.";
            return RedirectToAction("BankList");
        }

        public ActionResult DeleteBank(int id)
        {
            var bank = _bankService.GetById(id);
            if (bank == null)
                return HttpNotFound();

            _bankService.Delete(bank);
            TempData["Success"] = "Banka silindi.";
            return RedirectToAction("BankList");
        }

        // ===== BANK ACCOUNT CRUD =====

        public ActionResult BankAccountList()
        {
            var accounts = _bankAccountService.GetList();
            return View(accounts);
        }

        [HttpGet]
        public ActionResult AddBankAccount()
        {
            ViewBag.Banks = new SelectList(_bankService.GetList(), "BankId", "BankName");
            return View(new BankAccount());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBankAccount(BankAccount account)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Banks = new SelectList(_bankService.GetList(), "BankId", "BankName", account.BankId);
                return View(account);
            }

            _bankAccountService.Add(account);
            TempData["Success"] = "Banka hesabı eklendi.";
            return RedirectToAction("BankAccountList");
        }

        [HttpGet]
        public ActionResult EditBankAccount(int id)
        {
            var account = _bankAccountService.GetById(id);
            if (account == null) return HttpNotFound();

            ViewBag.Banks = new SelectList(_bankService.GetList(), "BankId", "BankName", account.BankId);
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBankAccount(BankAccount account)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Banks = new SelectList(_bankService.GetList(), "BankId", "BankName", account.BankId);
                return View(account);
            }

            _bankAccountService.Update(account);
            TempData["Success"] = "Banka hesabı güncellendi.";
            return RedirectToAction("BankAccountList");
        }

        public ActionResult DeleteBankAccount(int id)
        {
            var account = _bankAccountService.GetById(id);
            if (account == null) return HttpNotFound();

            _bankAccountService.Delete(account);
            TempData["Success"] = "Banka hesabı silindi.";
            return RedirectToAction("BankAccountList");
        }

        // ===== BANK TRANSACTION CRUD =====

        public ActionResult BankTransactionList()
        {
            var transactions = _transactionService.GetList();
            return View(transactions);
        }

        [HttpGet]
        public ActionResult AddBankTransaction()
        {
            ViewBag.Accounts = new SelectList(_bankAccountService.GetList(), "Id", "AccountName");
            ViewBag.Banks = new SelectList(_bankService.GetList(), "BankId", "BankName");
            return View(new BankTransaction());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBankTransaction(BankTransaction transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Accounts = new SelectList(_bankAccountService.GetList(), "Id", "AccountName", transaction.BankAccountId);
                ViewBag.Banks = new SelectList(_bankService.GetList(), "BankId", "BankName", transaction.BankId);
                return View(transaction);
            }

            _transactionService.Add(transaction);
            TempData["Success"] = "Banka hareketi eklendi.";
            return RedirectToAction("BankTransactionList");
        }

        [HttpGet]
        public ActionResult EditBankTransaction(int id)
        {
            var transaction = _transactionService.GetById(id);
            if (transaction == null) return HttpNotFound();

            ViewBag.Accounts = new SelectList(_bankAccountService.GetList(), "Id", "AccountName", transaction.BankAccountId);
            ViewBag.Banks = new SelectList(_bankService.GetList(), "BankId", "BankName", transaction.BankId);
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBankTransaction(BankTransaction transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Accounts = new SelectList(_bankAccountService.GetList(), "Id", "AccountName", transaction.BankAccountId);
                ViewBag.Banks = new SelectList(_bankService.GetList(), "BankId", "BankName", transaction.BankId);
                return View(transaction);
            }

            _transactionService.Update(transaction);
            TempData["Success"] = "Banka hareketi güncellendi.";
            return RedirectToAction("BankTransactionList");
        }

        public ActionResult DeleteBankTransaction(int id)
        {
            var transaction = _transactionService.GetById(id);
            if (transaction == null) return HttpNotFound();

            _transactionService.Delete(transaction);
            TempData["Success"] = "Banka hareketi silindi.";
            return RedirectToAction("BankTransactionList");
        }
    }
}
