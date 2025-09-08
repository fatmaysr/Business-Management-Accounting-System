using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;

namespace DataAccessLayer.EntityFramework
{
    public class EfStockDal : AppGenericRepository<Stock>, IStockDal
    {
    }
}
