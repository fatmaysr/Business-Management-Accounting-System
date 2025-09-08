using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IExchangeRateNewService
    {
        List<ExchangeRateNew> GetList();
        ExchangeRateNew GetById(int id);
        void Add(ExchangeRateNew entity);
        void Update(ExchangeRateNew entity);
        void Delete(ExchangeRateNew entity);
    }
}
