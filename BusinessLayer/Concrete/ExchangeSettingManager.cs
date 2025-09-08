using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class ExchangeSettingManager : IExchangeSettingService
    {
        private readonly IExchangeSettingDal _dal;
        public ExchangeSettingManager(IExchangeSettingDal dal) => _dal = dal;

        public void Add(ExchangeSetting entity) => _dal.Insert(entity);
        public void Delete(ExchangeSetting entity) => _dal.Delete(entity);
        public ExchangeSetting GetById(int id) => _dal.Get(x => x.Id == id);
        public List<ExchangeSetting> GetList() => _dal.List();
        public void Update(ExchangeSetting entity) => _dal.Update(entity);
    }
}
