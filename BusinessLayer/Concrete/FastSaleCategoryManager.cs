using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class FastSaleCategoryManager : IFastSaleCategoryService
    {
        private readonly IFastSaleCategoryDal _dal;
        public FastSaleCategoryManager(IFastSaleCategoryDal dal) => _dal = dal;

        public void Add(FastSaleCategory entity) => _dal.Insert(entity);
        public void Delete(FastSaleCategory entity) => _dal.Delete(entity);
        public FastSaleCategory GetById(int id) => _dal.Get(x => x.CategoryId == id);
        public FastSaleCategoryManager()
        {
            _dal = new EfFastSaleCategoryDal(); 
        }
        public List<FastSaleCategory> GetList() => _dal.List();
        public void Update(FastSaleCategory entity) => _dal.Update(entity);
    }
}
