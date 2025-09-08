using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IBankTransactionService
    {
        List<BankTransaction> GetList();
        BankTransaction GetById(int id);
        void Add(BankTransaction entity);
        void Update(BankTransaction entity);
        void Delete(BankTransaction entity);
    }
}
