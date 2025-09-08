using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class CaseTransactionManager : ICaseTransactionService
    {
        private readonly ICaseTransactionDal _dal;
        public CaseTransactionManager(ICaseTransactionDal dal) => _dal = dal;

        public void Add(CaseTransaction entity) => _dal.Insert(entity);
        public void Delete(CaseTransaction entity) => _dal.Delete(entity);
        public CaseTransaction GetById(int id) => _dal.Get(x => x.CaseTransactionId == id);
        public List<CaseTransaction> GetList() => _dal.List();
        public void Update(CaseTransaction entity) => _dal.Update(entity);
    }
}
