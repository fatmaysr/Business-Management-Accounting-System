using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IExchangeDataService
    {
        List<ExchangeData> GetList();
        ExchangeData GetById(int id);
        void Add(ExchangeData entity);
        void Update(ExchangeData entity);
        void Delete(ExchangeData entity);
    }
}
