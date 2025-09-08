using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class ExchangeRateNewManager : IExchangeRateNewService
    {
        private readonly IExchangeRateNewDal _dal;
        public ExchangeRateNewManager(IExchangeRateNewDal dal) => _dal = dal;

        public void Add(ExchangeRateNew entity) => _dal.Insert(entity);
        public void Delete(ExchangeRateNew entity) => _dal.Delete(entity);
        public ExchangeRateNew GetById(int id) => _dal.Get(x => x.Id == id);
        public List<ExchangeRateNew> GetList() => _dal.List();
        public void Update(ExchangeRateNew entity) => _dal.Update(entity);
    }
}
