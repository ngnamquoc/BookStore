using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;

        }        //public void Add(Category entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Category Get(Expression<Func<Category, bool>> filter)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Category> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Remove(Category entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveRange(IEnumerable<Category> entity)
        //{
        //    throw new NotImplementedException();
        //}

        

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
