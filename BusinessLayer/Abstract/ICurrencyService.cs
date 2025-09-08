using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ICurrencyService
    {
        List<Currency> GetList();
        Currency GetById(int id);
        void Add(Currency entity);
        void Update(Currency entity);
        void Delete(Currency entity);
    }
}
