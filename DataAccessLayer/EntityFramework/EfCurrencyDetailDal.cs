using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using DataAccessLayer.Concrete;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.EntityFramework
{
    public class EfCurrencyDetailDal : AppGenericRepository<CurrencyDetail>, ICurrencyDetailDal
    {
        // CurrencyId bazlı detayları Customer ile birlikte al
        public List<CurrencyDetail> GetListWithCustomer(int currencyId)
        {
            using (var context = new AppDbContext())
            {
                return context.CurrencyDetails
                              .Include(d => d.Customer) // Customer ilişkisini dahil et
                              .Where(d => d.CariID == currencyId)
                              .ToList();
            }
        }
    }
}