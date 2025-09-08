using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IFastSaleCategoryService
    {
        List<FastSaleCategory> GetList();
        FastSaleCategory GetById(int id);
        void Add(FastSaleCategory entity);
        void Update(FastSaleCategory entity);
        void Delete(FastSaleCategory entity);
    }
}
