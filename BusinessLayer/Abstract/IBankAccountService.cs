using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IBankAccountService
    {
        List<BankAccount> GetList();
        BankAccount GetById(int id);
        void Add(BankAccount entity);
        void Update(BankAccount entity);
        void Delete(BankAccount entity);
    }
}
