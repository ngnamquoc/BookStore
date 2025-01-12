using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        //add 2 more functionalities - update and save
        void Update(Category obj);
    }
}
