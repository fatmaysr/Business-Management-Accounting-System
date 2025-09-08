using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class DatsManager : IDatsService
    {
        private readonly IDatsDal _dal;
        public DatsManager(IDatsDal dal) => _dal = dal;

        public void Add(Dats entity) => _dal.Insert(entity);
        public void Delete(Dats entity) => _dal.Delete(entity);
        public Dats GetById(int id) => _dal.Get(x => x.Id == id);
        public List<Dats> GetList() => _dal.List();
        public void Update(Dats entity) => _dal.Update(entity);
    }
}
