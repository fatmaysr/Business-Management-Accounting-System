using Xunit;
using Moq;
using BusinessLayer.Concrete;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Tests
{
    public class SaleManagerTests
    {
        [Fact]
        public void CompleteSale_ShouldThrowException_WhenProductNotFound()
        {
            // Arrange (Hazırlık)
            var mockDal = new Mock<ISaleDal>();
            var mockProductService = new Mock<IFastSaleProductService>();
            var mockCashCaseService = new Mock<ICashCaseService>();
            var mockTransactionService = new Mock<ICaseTransactionService>();
            var mockCustomerService = new Mock<ICustomerService>();
            var mockUserService = new Mock<IUserService>();

            // Ürün listesini boş döndürüyoruz ki ürün bulunamasın
            mockProductService.Setup(p => p.GetList()).Returns(new List<FastSaleProduct>());

            var saleManager = new SaleManager(
                mockDal.Object,
                mockProductService.Object,
                mockCashCaseService.Object,
                mockTransactionService.Object,
                mockCustomerService.Object,
                mockUserService.Object
            );

            // Act & Assert (Çalıştırma ve Doğrulama)
            var exception = Assert.Throws<InvalidOperationException>(() =>
                saleManager.CompleteSale("OlmayanUrun", "Nakit", "admin")
            );

            Assert.Contains("adlı ürün bulunamadı", exception.Message);
        }

        [Fact]
        public void CompleteSale_ShouldCreateSale_WhenParametersAreValid()
        {
            // Arrange (Hazırlık)
            var mockDal = new Mock<ISaleDal>();
            var mockProductService = new Mock<IFastSaleProductService>();
            var mockCashCaseService = new Mock<ICashCaseService>();
            var mockTransactionService = new Mock<ICaseTransactionService>();
            var mockCustomerService = new Mock<ICustomerService>();
            var mockUserService = new Mock<IUserService>();

            // Mock verilerini hazırlıyoruz
            var testProduct = new FastSaleProduct { ProductId = 1, ProductName = "Altin Kolye", Value = "1500,00" };
            var testCase = new CashCase { CaseId = 1, Status = 1, Currency = "TL" };
            var testCustomer = new Customer { CariID = 5, CariKodu = "PERAKENDE" };

            mockProductService.Setup(p => p.GetList()).Returns(new List<FastSaleProduct> { testProduct });
            mockCashCaseService.Setup(c => c.GetList()).Returns(new List<CashCase> { testCase });
            mockCustomerService.Setup(c => c.GetList()).Returns(new List<Customer> { testCustomer });
            mockUserService.Setup(u => u.GetList()).Returns(new List<EntityLayer.Concrete.User>());

            Sale? insertedSale = null;
            mockDal.Setup(d => d.Insert(It.IsAny<Sale>())).Callback<Sale>(s => insertedSale = s);

            var saleManager = new SaleManager(
                mockDal.Object,
                mockProductService.Object,
                mockCashCaseService.Object,
                mockTransactionService.Object,
                mockCustomerService.Object,
                mockUserService.Object
            );

            // Act (Çalıştırma)
            var result = saleManager.CompleteSale("Altin Kolye", "Nakit", "admin");

            // Assert (Doğrulama)
            Assert.NotNull(result);
            Assert.Equal("Altin Kolye", result.ProductName);
            Assert.Equal(1500.00m, result.Price);
            Assert.Equal("Nakit", result.PaymentType);

            // Kasaya transaction (hareket) eklenmiş mi kontrol ediyoruz
            mockTransactionService.Verify(t => t.Add(It.IsAny<CaseTransaction>()), Times.Once);
        }
    }
}