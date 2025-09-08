using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ICustomerService
    {
        List<Customer> GetList();
        Customer GetById(int id);
        void Add(Customer entity);
        void Update(Customer entity);
        void Delete(Customer entity);
    }
}
