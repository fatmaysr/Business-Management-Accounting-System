using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GateGold.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;


        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        public ActionResult CustomerList()
        {
            var customers = _customerService.GetList();
            return View(customers);
        }

        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View(new Customer());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(Customer model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _customerService.Add(model);
            TempData["Success"] = "Müşteri başarıyla eklendi.";
            return RedirectToAction("CustomerList");
        }



        [HttpGet]
        public ActionResult EditCustomer(int id)
        {


            var customer = _customerService.GetById(id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(Customer model)
        {
            // ModelState.IsValid kontrolü sayesinde Validator kuralları uygulanır
            if (!ModelState.IsValid)
            {
                // Hatalı giriş varsa view'a geri dönülür ve ValidationMessageFor ile gösterilir
                return View(model);
            }

            // Model geçerliyse güncelleme yapılır
            _customerService.Update(model);
            TempData["Success"] = "Müşteri bilgileri başarıyla güncellendi.";

            // Liste sayfasına yönlendirme
            return RedirectToAction("CustomerList");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
                return HttpNotFound();

            _customerService.Delete(customer);
            TempData["Success"] = "Müşteri silindi.";
            return RedirectToAction("CustomerList");
        }




        public ActionResult DebtReport()
        {
            var customers = _customerService.GetList();
            if (customers == null) customers = new List<Customer>();
            ViewBag.Customers = customers.Select(c => new SelectListItem
            {
                Value = c.CariID.ToString(),
                Text = c.CariAdi
            }).ToList();
            return View();
        }


        public ActionResult Balance()
        {
            var customers = _customerService.GetList() ?? new List<Customer>();
            ViewBag.Customers = customers.Select(c => new SelectListItem
            {
                Value = c.CariID.ToString(),
                Text = c.CariAdi
            }).ToList();
            return View();
        }

        public ActionResult TransactionReport()
        {
            var customers = _customerService.GetList() ?? new List<Customer>();
            ViewBag.Customers = customers.Select(c => new SelectListItem
            {
                Value = c.CariID.ToString(),
                Text = c.CariAdi
            }).ToList();
            return View();
        }


        public ActionResult ExportToExcel()
        {
            var customers = _customerService.GetList();

            var mappings = new Dictionary<string, string>
    {
        { "CariID", "Cari ID" },
        { "CariAdi", "Cari Unvanı / Adı" },
        { "CariKodu", "Cari Kodu" },
        { "CariTipi", "Cari Tipi" },
        { "Telefon", "Telefon" },
        { "Email", "E-Posta" }
    };

            var excelService = new ExcelExportService();
            byte[] fileBytes = excelService.ExportToExcel(customers, "Cari_Listesi", mappings);

            string fileName = $"Cari_Listesi_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
}