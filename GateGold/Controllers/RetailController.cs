using System.Web.Mvc;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete; // Somut managerlar için
using EntityLayer.Concrete;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Nexus.Controllers
{
    public class RetailController : Controller
    {
        private readonly IFastSaleCategoryService _categoryService;
        private readonly IFastSaleProductService _productService;
        private readonly ISaleService _saleService;

        public RetailController(
            IFastSaleCategoryService categoryService,
            IFastSaleProductService productService,
            ISaleService saleService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _saleService = saleService;
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

        public ActionResult Payment(string category, string product)
        {
            TempData["Category"] = category;
            TempData["Product"] = product;
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedProduct = product;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessPayment(string category, string product, string paymentType)
        {
            if (paymentType == "Kart")
            {
                TempData["Category"] = category;
                TempData["Product"] = product;
                TempData["PaymentType"] = "Kart";
                return RedirectToAction("CardPayment");
            }

            if (paymentType == "Nakit")
            {
                try
                {
                    var sale = _saleService.CompleteSale(product, "Nakit", User.Identity.Name);
                    return RedirectToAction("Complete", new { saleId = sale.SaleId });
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.SelectedCategory = category;
                    ViewBag.SelectedProduct = product;
                    return View("Payment");
                }
            }

            ModelState.AddModelError("", "Lütfen bir ödeme yöntemi seçin.");
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedProduct = product;
            return View("Payment");
        }

        public ActionResult CardPayment()
        {
            ViewBag.Category = TempData["Category"];
            ViewBag.Product = TempData["Product"];
            TempData.Keep();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CardPayment(string category, string product, string cardNumber, string expiryDate, string cvv)
        {
            try
            {
                var sale = _saleService.CompleteSale(product, "Kart", User.Identity.Name);
                return RedirectToAction("Complete", new { saleId = sale.SaleId });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Category = category;
                ViewBag.Product = product;
                return View("CardPayment");
            }
        }

        public ActionResult Complete(int saleId)
        {
            var sale = _saleService.GetById(saleId);
            if (sale == null)
                return HttpNotFound();

            return View(sale);
        }
    }
}
