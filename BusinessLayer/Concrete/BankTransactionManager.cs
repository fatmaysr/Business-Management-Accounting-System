using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class BankTransactionManager : IBankTransactionService
    {
        private readonly IBankTransactionDal _dal;
        public BankTransactionManager(IBankTransactionDal dal) => _dal = dal;

        public void Add(BankTransaction entity) => _dal.Insert(entity);
        public void Delete(BankTransaction entity) => _dal.Delete(entity);
        public BankTransaction GetById(int id) => _dal.Get(x => x.Id == id);
        public List<BankTransaction> GetList() => _dal.List();
        public void Update(BankTransaction entity) => _dal.Update(entity);
    }
}
