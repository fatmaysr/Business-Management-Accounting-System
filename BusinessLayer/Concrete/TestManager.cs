using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class TestManager : ITestService
    {
        private readonly ITestDal _dal;
        public TestManager(ITestDal dal) => _dal = dal;

        public void Add(Test entity) => _dal.Insert(entity);
        public void Delete(Test entity) => _dal.Delete(entity);
        public Test GetById(int id) => _dal.Get(x => x.Id == id);
        public List<Test> GetList() => _dal.List();
        public void Update(Test entity) => _dal.Update(entity);
    }
}
