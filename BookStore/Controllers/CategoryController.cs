using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        //local variable
        private readonly ApplicationDbContext _db;
        //constructor
        public CategoryController(ApplicationDbContext db)
        {
            //assigning the db context to the local variable
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
    }
}
