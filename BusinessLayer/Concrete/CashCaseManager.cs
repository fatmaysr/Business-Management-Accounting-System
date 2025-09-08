using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class CashCaseManager : ICashCaseService
    {
        private readonly ICashCaseDal _dal;
        public CashCaseManager(ICashCaseDal dal) => _dal = dal;

        public void Add(CashCase entity) => _dal.Insert(entity);
        public void Delete(CashCase entity) => _dal.Delete(entity);
        public CashCase GetById(int id) => _dal.Get(x => x.CaseId == id);
        public List<CashCase> GetList() => _dal.List();
        public void Update(CashCase entity) => _dal.Update(entity);
    }
}
