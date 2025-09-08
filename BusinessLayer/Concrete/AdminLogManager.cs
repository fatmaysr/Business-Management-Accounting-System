using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class AdminLogManager : IAdminLogService
    {
        private readonly IAdminLogDal _dal;

        public AdminLogManager(IAdminLogDal dal) => _dal = dal;

        public void Add(AdminLog entity) => _dal.Insert(entity);
        public void Delete(AdminLog entity) => _dal.Delete(entity);
        public AdminLog GetById(int id) => _dal.Get(x => x.Id == id);
        public List<AdminLog> GetList() => _dal.List();
        public void Update(AdminLog entity) => _dal.Update(entity);
    }
}
