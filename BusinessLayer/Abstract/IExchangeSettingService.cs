using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IExchangeSettingService
    {
        List<ExchangeSetting> GetList();
        ExchangeSetting GetById(int id);
        void Add(ExchangeSetting entity);
        void Update(ExchangeSetting entity);
        void Delete(ExchangeSetting entity);
    }
}
