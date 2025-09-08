using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ICurrencyDetailService
    {
        List<CurrencyDetail> GetList();
        CurrencyDetail GetById(int id);
        void Add(CurrencyDetail entity);
        void Update(CurrencyDetail entity);
        void Delete(CurrencyDetail entity);
    }
}
