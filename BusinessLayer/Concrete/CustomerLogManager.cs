using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class CustomerLogManager : ICustomerLogService
    {
        private readonly ICustomerLogDal _dal;
        public CustomerLogManager(ICustomerLogDal dal) => _dal = dal;

        public void Add(CustomerLog entity) => _dal.Insert(entity);
        public void Delete(CustomerLog entity) => _dal.Delete(entity);
        public CustomerLog GetById(int id) => _dal.Get(x => x.Id == id);
        public List<CustomerLog> GetList() => _dal.List();
        public void Update(CustomerLog entity) => _dal.Update(entity);
    }
}
