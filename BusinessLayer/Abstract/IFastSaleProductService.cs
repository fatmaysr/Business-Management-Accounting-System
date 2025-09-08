using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IFastSaleProductService
    {
        List<FastSaleProduct> GetList();
        FastSaleProduct GetById(int id);
        void Add(FastSaleProduct entity);
        void Update(FastSaleProduct entity);
        void Delete(FastSaleProduct entity);
    }
}
