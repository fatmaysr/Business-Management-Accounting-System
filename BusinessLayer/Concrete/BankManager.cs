using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class BankManager : IBankService
    {
        private readonly IBankDal _dal;
        public BankManager(IBankDal dal) => _dal = dal;

        public void Add(Bank entity) => _dal.Insert(entity);
        public void Delete(Bank entity) => _dal.Delete(entity);
        public Bank GetById(int id) => _dal.Get(x => x.BankId == id);
        public List<Bank> GetList() => _dal.List();
        public void Update(Bank entity) => _dal.Update(entity);
    }
}
