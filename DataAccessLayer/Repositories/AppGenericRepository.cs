using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Concrete;
using DataAccessLayer.Abstract;


namespace DataAccessLayer.Repositories
{
    public class AppGenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public AppGenericRepository()
        {
            _context = new AppDbContext();
            _dbSet = _context.Set<T>();
        }

        public List<T> List()
        {
            return _dbSet.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter).ToList();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _dbSet.FirstOrDefault(filter);
        }

        public void Insert(T p)
        {
            _dbSet.Add(p);
            _context.SaveChanges();
        }

        public void Update(T p)
        {
            _context.Entry(p).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T p)
        {
            _dbSet.Remove(p);
            _context.SaveChanges();
        }
    }
}
