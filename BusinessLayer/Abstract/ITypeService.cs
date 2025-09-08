using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ITypeService
    {
        List<Type> GetList();
        Type GetById(int id);
        void Add(Type entity);
        void Update(Type entity);
        void Delete(Type entity);
    }
}
