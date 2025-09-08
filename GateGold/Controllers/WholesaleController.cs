using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Web.Mvc;

namespace GateGold.Controllers
{
    public class WholesaleController : Controller
    {
        StockManager sm = new StockManager(new EfStockDal());

        public ActionResult StockList()
        {
            var stocks = sm.GetList();
            return View(stocks);
        }

        public ActionResult AddStock()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStock(Stock stock)
        {
            stock.CreatedDate = DateTime.Now;

            StockValidator validator = new StockValidator();
            var results = validator.Validate(stock);

            if (results.IsValid)
            {
                sm.Add(stock);
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
            var stock = sm.GetById(id);
            return View(stock);
        }

        [HttpPost]
        public ActionResult EditStock(Stock stock)
        {
            StockValidator validator = new StockValidator();
            var results = validator.Validate(stock);

            if (results.IsValid)
            {
                sm.Update(stock);
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
            var stock = sm.GetById(id);
            sm.Delete(stock);
            return RedirectToAction("StockList");
        }
    }
}
