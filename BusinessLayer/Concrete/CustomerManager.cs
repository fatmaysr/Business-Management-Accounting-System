using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _dal;
        public CustomerManager(ICustomerDal dal) => _dal = dal;

        public void Add(Customer entity) => _dal.Insert(entity);
        public void Delete(Customer entity) => _dal.Delete(entity);
        public Customer GetById(int id) => _dal.Get(x => x.CariID == id);
        public List<Customer> GetList() => _dal.List();
        public void Update(Customer entity) => _dal.Update(entity);
    }
}
