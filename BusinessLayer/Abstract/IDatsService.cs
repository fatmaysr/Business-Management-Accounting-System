using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IDatsService
    {
        List<Dats> GetList();
        Dats GetById(int id);
        void Add(Dats entity);
        void Update(Dats entity);
        void Delete(Dats entity);
    }
}
