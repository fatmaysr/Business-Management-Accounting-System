using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class BankAccountManager : IBankAccountService
    {
        private readonly IBankAccountDal _dal;
        public BankAccountManager(IBankAccountDal dal) => _dal = dal;

        public void Add(BankAccount entity) => _dal.Insert(entity);
        public void Delete(BankAccount entity) => _dal.Delete(entity);
        public BankAccount GetById(int id) => _dal.Get(x => x.BankAccountId == id);
        public List<BankAccount> GetList() => _dal.List();
        public void Update(BankAccount entity) => _dal.Update(entity);
    }
}
