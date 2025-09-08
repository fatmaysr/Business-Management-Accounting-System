using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IExchangeRateService
    {
        List<ExchangeRate> GetList();
        ExchangeRate GetById(int id);
        void Add(ExchangeRate entity);
        void Update(ExchangeRate entity);
        void Delete(ExchangeRate entity);
    }
}
