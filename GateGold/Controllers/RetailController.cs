using System.Web.Mvc;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete; // Somut managerlar için
using EntityLayer.Concrete;
using System.Linq;
using System.Collections.Generic;
using System;

namespace GateGold.Controllers
{
    public class RetailController : Controller
    {
        private readonly IFastSaleCategoryService _categoryService;
        private readonly IFastSaleProductService _productService;


        // ---------------- Parametreli Constructor ----------------
        public RetailController(
            IFastSaleCategoryService categoryService,
            IFastSaleProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        // -------------------- Kategori CRUD --------------------
        public ActionResult AllCategories()
        {
            List<FastSaleCategory> categories = _categoryService.GetList();
            return View(categories);
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                ModelState.AddModelError("", "Kategori adı boş olamaz.");
                return View();
            }

            var category = new FastSaleCategory
            {
                CategoryName = categoryName,
                Date = DateTime.Now,
                Status = 1,
                UserId = 1
            };

            _categoryService.Add(category);
            return RedirectToAction("AllCategories");
        }

        public ActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (category != null)
                _categoryService.Delete(category);

            return RedirectToAction("AllCategories");
        }

        public ActionResult EditCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(FastSaleCategory category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryService.Update(category);
            return RedirectToAction("AllCategories");
        }

        // -------------------- Ürün CRUD --------------------
        public ActionResult AllProducts()
        {
            var products = _productService.GetList(); // Tüm ürünleri getir
            return View(products);
        }


        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.Categories = _categoryService.GetList(); // Kategorileri getir
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(FastSaleProduct product)
        {
            if (ModelState.IsValid)
            {
                product.Date = DateTime.Now; // Tarih ekle
                _productService.Add(product);
                return RedirectToAction("AllProducts");
            }

           
            ViewBag.Categories = _categoryService.GetList();
            return View(product);
        }


        public ActionResult EditProduct(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var product = _productService.GetById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = _categoryService.GetList();
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(FastSaleProduct product)
        {
            if (ModelState.IsValid)
            {
                _productService.Update(product);
                return RedirectToAction("AllProducts");
            }
            ViewBag.Categories = _categoryService.GetList();
            return View(product);
        }

        public ActionResult DeleteProduct(int id)
        {
            var product = _productService.GetById(id);
            if (product != null)
                _productService.Delete(product);

            return RedirectToAction("AllProducts");
        }

        // -------------------- QuickSale / Payment Flow --------------------
        public ActionResult QuickSale()
        {
            ViewBag.Categories = _categoryService.GetList();
            return View();
        }

        public ActionResult ProductSelection(string category)
        {
            var products = _productService.GetList()
                .Where(p => p.Category == category)
                .Select(p => p.ProductName)
                .ToList();

            ViewBag.SelectedCategory = category;
            ViewBag.Products = products;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmSale(string category, string product)
        {
            TempData["Category"] = category;
            TempData["Product"] = product;
            return RedirectToAction("Payment");
        }

        public ActionResult Payment()
        {
            ViewBag.SelectedCategory = TempData["Category"];
            ViewBag.SelectedProduct = TempData["Product"];
            TempData.Keep();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessPayment(string category, string product, string paymentType)
        {
            TempData["Category"] = category;
            TempData["Product"] = product;
            TempData["PaymentType"] = paymentType;

            if (paymentType == "Nakit")
                return RedirectToAction("Complete");
            else if (paymentType == "Kart")
                return RedirectToAction("CardPayment");

            ModelState.AddModelError("", "Lütfen bir ödeme yöntemi seçin.");
            return View("Payment");
        }

        public ActionResult CardPayment()
        {
            TempData.Keep();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CardPayment(string category, string product, string cardNumber, string expiryDate, string cvv)
        {
            TempData["Category"] = category;
            TempData["Product"] = product;
            TempData["PaymentType"] = "Kart";
            return RedirectToAction("Complete");
        }

        public ActionResult Complete()
        {
            ViewBag.Category = TempData["Category"];
            ViewBag.Product = TempData["Product"];
            ViewBag.PaymentType = TempData["PaymentType"];
            return View();
        }
    }
}
