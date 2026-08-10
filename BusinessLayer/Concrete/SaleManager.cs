using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BusinessLayer.Concrete
{
    public class SaleManager : ISaleService
    {
        private readonly ISaleDal _dal;
        private readonly IFastSaleProductService _productService;
        private readonly ICashCaseService _cashCaseService;
        private readonly ICaseTransactionService _caseTransactionService;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public SaleManager(
            ISaleDal dal,
            IFastSaleProductService productService,
            ICashCaseService cashCaseService,
            ICaseTransactionService caseTransactionService,
            ICustomerService customerService,
            IUserService userService)
        {
            _dal = dal;
            _productService = productService;
            _cashCaseService = cashCaseService;
            _caseTransactionService = caseTransactionService;
            _customerService = customerService;
            _userService = userService;
        }

        public void Add(Sale sale) => _dal.Insert(sale);
        public Sale GetById(int id) => _dal.Get(x => x.SaleId == id);
        public List<Sale> GetList() => _dal.List();

        /// <summary>
        /// Perakende satışı uçtan uca tamamlar: ürünü bulur, fiyatı okur,
        /// Sale kaydı oluşturur ve tutarı aktif kasaya "Giriş" hareketi olarak yazar.
        /// </summary>
        public Sale CompleteSale(string productName, string paymentType, string username)
        {
            var product = _productService.GetList()
                .FirstOrDefault(p => p.ProductName == productName);
            if (product == null)
                throw new InvalidOperationException($"'{productName}' adlı ürün bulunamadı.");

            if (!decimal.TryParse(
                    product.Value?.Replace(",", "."),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var price))
                throw new InvalidOperationException($"'{productName}' ürününün fiyatı okunamadı.");

            var cashCase = _cashCaseService.GetList().FirstOrDefault(c => c.Status == 1);
            if (cashCase == null)
                throw new InvalidOperationException("Aktif bir kasa bulunamadı. Önce Kasa modülünden bir kasa oluşturun.");

            var customer = _customerService.GetList().FirstOrDefault(c => c.CariKodu == "PERAKENDE");
            if (customer == null)
                throw new InvalidOperationException("Perakende müşteri cari kaydı bulunamadı. Migration seed adımını kontrol edin.");

            var user = _userService.GetList().FirstOrDefault(u => u.UserName == username);

            var sale = new Sale
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = price,
                PaymentType = paymentType,
                CaseId = cashCase.CaseId,
                CariID = customer.CariID,
                UserId = user?.UserId ?? 0,
                Date = DateTime.Now
            };
            _dal.Insert(sale);

            var transaction = new CaseTransaction
            {
                CaseId = cashCase.CaseId,
                CariID = customer.CariID,
                Date = DateTime.Now,
                Type = "Giriş",
                Amount = price,
                Currency = cashCase.Currency,
                ExchangeRate = 1,
                Description = $"Perakende satış - {product.ProductName}",
                UserId = user?.UserId ?? 0
            };
            _caseTransactionService.Add(transaction);

            return sale;
        }
    }
}