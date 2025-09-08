using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{

    public class StockManager : IStockService
    {
        private readonly IStockDal _dal;
        public StockManager(IStockDal dal) => _dal = dal;

        public void Add(Stock c) => _dal.Insert(c);
        public void Delete(Stock c) => _dal.Delete(c);
        public Stock GetById(int id) => _dal.Get(x => x.StockId == id);
        public List<Stock> GetList() => _dal.List();
        public void Update(Stock c) => _dal.Update(c);
    }
}
