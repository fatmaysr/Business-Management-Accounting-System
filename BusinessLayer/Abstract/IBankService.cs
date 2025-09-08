using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IBankService
    {
        List<Bank> GetList();
        Bank GetById(int id);
        void Add(Bank entity);
        void Update(Bank entity);
        void Delete(Bank entity);
    }
}
