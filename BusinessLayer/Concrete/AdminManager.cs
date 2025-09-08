using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class AdminManager : IAdminService
    {
        private readonly IAdminDal _dal;
        public AdminManager(IAdminDal dal) => _dal = dal;

        public void Add(Admin c) => _dal.Insert(c);
        public void Delete(Admin c) => _dal.Delete(c);
        public Admin GetById(int id) => _dal.Get(x => x.Id == id);
        public List<Admin> GetList() => _dal.List();
        public void Update(Admin c) => _dal.Update(c);
    }
}
