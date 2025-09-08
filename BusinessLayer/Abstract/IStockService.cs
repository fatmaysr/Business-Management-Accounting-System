using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntityLayer.Concrete;


namespace BusinessLayer.Abstract
{
    public interface IStockService
    {
        void Add(Stock stock);
        void Update(Stock stock);
        void Delete(Stock stock);
        List<Stock> GetList();
        Stock GetById(int id);
    }
}
