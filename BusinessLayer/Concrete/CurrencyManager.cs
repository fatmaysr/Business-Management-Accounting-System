using BusinessLayer.Abstract;

using DataAccessLayer.Abstract;

using EntityLayer.Concrete;

using System.Collections.Generic;



namespace BusinessLayer.Concrete

{

    public class CurrencyManager : ICurrencyService

    {

        private readonly ICurrencyDal _dal;

        public CurrencyManager(ICurrencyDal dal) => _dal = dal;



        public void Add(Currency entity) => _dal.Insert(entity);

        public void Delete(Currency entity) => _dal.Delete(entity);

        public Currency GetById(int id) => _dal.Get(x => x.CurrencyId == id);

        public List<Currency> GetList() => _dal.List();

        public void Update(Currency entity) => _dal.Update(entity);

    }

}