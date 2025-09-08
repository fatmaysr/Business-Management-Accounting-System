using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ICashCaseService
    {
        List<CashCase> GetList();
        CashCase GetById(int id);
        void Add(CashCase entity);
        void Update(CashCase entity);
        void Delete(CashCase entity);
    }
}
