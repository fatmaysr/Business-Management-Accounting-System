using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IFastSaleService
    {
        List<FastSale> GetList();
        FastSale GetById(int id);
        void Add(FastSale entity);
        void Update(FastSale entity);
        void Delete(FastSale entity);
    }
}
