using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class TypeManager : ITypeService
    {
        private readonly ITypeDal _dal;
        public TypeManager(ITypeDal dal) => _dal = dal;

        public void Add(Type entity) => _dal.Insert(entity);
        public void Delete(Type entity) => _dal.Delete(entity);
        public Type GetById(int id) => _dal.Get(x => x.Id == id);
        public List<Type> GetList() => _dal.List();
        public void Update(Type entity) => _dal.Update(entity);
    }
}
