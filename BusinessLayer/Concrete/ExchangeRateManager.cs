using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class ExchangeRateManager : IExchangeRateService
    {
        private readonly IExchangeRateDal _dal;
        public ExchangeRateManager(IExchangeRateDal dal) => _dal = dal;

        public void Add(ExchangeRate entity) => _dal.Insert(entity);
        public void Delete(ExchangeRate entity) => _dal.Delete(entity);
        public ExchangeRate GetById(int id) => _dal.Get(x => x.Id == id);
        public List<ExchangeRate> GetList() => _dal.List();
        public void Update(ExchangeRate entity) => _dal.Update(entity);
    }
}
