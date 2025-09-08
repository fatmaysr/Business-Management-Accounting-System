using BusinessLayer.Abstract;

using DataAccessLayer.Abstract;

using EntityLayer.Concrete;

using System.Collections.Generic;



namespace BusinessLayer.Concrete

{

    public class CurrencyDetailManager : ICurrencyDetailService

    {

        private readonly ICurrencyDetailDal _dal;

        public CurrencyDetailManager(ICurrencyDetailDal dal) => _dal = dal;



        public void Add(CurrencyDetail entity) => _dal.Insert(entity);

        public void Delete(CurrencyDetail entity) => _dal.Delete(entity);

        public CurrencyDetail GetById(int id) => _dal.Get(x => x.CurrecyDetailId == id);

        public List<CurrencyDetail> GetList() => _dal.List();

        public void Update(CurrencyDetail entity) => _dal.Update(entity);

    }

}