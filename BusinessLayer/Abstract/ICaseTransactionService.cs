using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ICaseTransactionService
    {
        List<CaseTransaction> GetList();
        CaseTransaction GetById(int id);
        void Add(CaseTransaction entity);
        void Update(CaseTransaction entity);
        void Delete(CaseTransaction entity);
    }
}
