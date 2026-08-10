using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Web.Mvc;
using BusinessLayer.Abstract;

namespace GateGold.Controllers
{
    public class WholesaleController : Controller
    {
        private readonly IStockService _stockService;

        public WholesaleController(IStockService stockService)
        {
            _stockService = stockService;
        }

        public ActionResult StockList()
        {
            var stocks = _stockService.GetList();
            return View(stocks);
        }

        public ActionResult AddStock()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStock(Stock stock)
        {
            stock.CreatedDate = DateTime.Now;

            StockValidator validator = new StockValidator();
            var results = validator.Validate(stock);

            if (results.IsValid)
            {
                _stockService.Add(stock);
                return RedirectToAction("StockList");
            }
            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(stock);
            }
        }

        public ActionResult EditStock(int id)
        {
            var stock = _stockService.GetById(id);
            return View(stock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStock(Stock stock)
        {
            StockValidator validator = new StockValidator();
            var results = validator.Validate(stock);

            if (results.IsValid)
            {
                _stockService.Update(stock);
                return RedirectToAction("StockList");
            }
            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(stock);
            }
        }

        public ActionResult DeleteStock(int id)
        {
            var stock = _stockService.GetById(id);
            _stockService.Delete(stock);
            return RedirectToAction("StockList");
        }
    }
}
