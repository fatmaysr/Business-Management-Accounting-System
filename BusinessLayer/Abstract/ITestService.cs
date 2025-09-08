using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ITestService
    {
        List<Test> GetList();
        Test GetById(int id);
        void Add(Test entity);
        void Update(Test entity);
        void Delete(Test entity);
    }
}
