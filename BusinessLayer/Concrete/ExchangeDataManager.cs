using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class ExchangeDataManager : IExchangeDataService
    {
        private readonly IExchangeDataDal _dal;
        public ExchangeDataManager(IExchangeDataDal dal) => _dal = dal;

        public void Add(ExchangeData entity) => _dal.Insert(entity);
        public void Delete(ExchangeData entity) => _dal.Delete(entity);
        public ExchangeData GetById(int id) => _dal.Get(x => x.Id == id);
        public List<ExchangeData> GetList() => _dal.List();
        public void Update(ExchangeData entity) => _dal.Update(entity);
    }
}
