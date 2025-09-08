using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ICustomerLogService
    {
        List<CustomerLog> GetList();
        CustomerLog GetById(int id);
        void Add(CustomerLog entity);
        void Update(CustomerLog entity);
        void Delete(CustomerLog entity);
    }
}
