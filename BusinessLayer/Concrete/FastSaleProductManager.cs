using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class FastSaleProductManager : IFastSaleProductService
    {
        private readonly IFastSaleProductDal _dal;
        public FastSaleProductManager(IFastSaleProductDal dal) => _dal = dal;

        public void Add(FastSaleProduct entity) => _dal.Insert(entity);
        public void Delete(FastSaleProduct entity) => _dal.Delete(entity);
        public FastSaleProduct GetById(int id) => _dal.Get(x => x.ProductId == id);
        public List<FastSaleProduct> GetList() => _dal.List();
        public void Update(FastSaleProduct entity) => _dal.Update(entity);
    }
}
