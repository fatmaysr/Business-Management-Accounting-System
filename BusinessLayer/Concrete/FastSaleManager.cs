using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class FastSaleManager : IFastSaleService
    {
        private readonly IFastSaleDal _dal;
        public FastSaleManager(IFastSaleDal dal) => _dal = dal;

        public void Add(FastSale entity) => _dal.Insert(entity);
        public void Delete(FastSale entity) => _dal.Delete(entity);
        public FastSale GetById(int id) => _dal.Get(x => x.FastSaleId == id);
        public List<FastSale> GetList() => _dal.List();
        public void Update(FastSale entity) => _dal.Update(entity);
    }
}
