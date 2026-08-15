using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;

namespace GateGold
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IBankDal, EfBankDal>();
            container.RegisterType<IBankAccountDal, EfBankAccountDal>();
            container.RegisterType<IBankTransactionDal, EfBankTransactionDal>();
            container.RegisterType<ICaseTransactionDal, EfCaseTransaction>();
            container.RegisterType<ICashCaseDal, EfCashCaseDal>();
            container.RegisterType<ICustomerDal, EfCustomerDal>();
            container.RegisterType<IFastSaleDal, EfFastSaleDal>();
            container.RegisterType<IFastSaleCategoryDal, EfFastSaleCategoryDal>();
            container.RegisterType<IFastSaleProductDal, EfFastSaleProductDal>();
            container.RegisterType<IStockDal, EfStockDal>();
            container.RegisterType<IUserDal, EfUserDal>();
            container.RegisterType<ISaleDal, EfSaleDal>();
            container.RegisterType<ISaleService, SaleManager>();
            container.RegisterType<IBankService, BankManager>();
            container.RegisterType<IBankAccountService, BankAccountManager>();
            container.RegisterType<IBankTransactionService, BankTransactionManager>();
            container.RegisterType<ICaseTransactionService, CaseTransactionManager>();
            container.RegisterType<ICashCaseService, CashCaseManager>();
            container.RegisterType<ICustomerService, CustomerManager>();
            container.RegisterType<IFastSaleService, FastSaleManager>();
            container.RegisterType<IFastSaleCategoryService, FastSaleCategoryManager>();
            container.RegisterType<IFastSaleProductService, FastSaleProductManager>();
            container.RegisterType<IStockService, StockManager>();
            container.RegisterType<IUserService, UserManager>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}