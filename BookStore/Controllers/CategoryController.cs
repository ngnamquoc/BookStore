using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name== obj.DisplayOrder.ToString())
            {
                //add custom err mess
                ModelState.AddModelError("name", "The Display Order cannot be the same as the Category Name");
            }
           

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["successMess"] = "Category created successfully";
                return RedirectToAction("Index", "Category");


            }
            return View();


        }

        public IActionResult Edit(int id)
        {
            if (id==null || id ==0)
            {
                return NotFound();

            }

            //search by id
            Category? categoryFromDb= _db.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            //server side validation
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                //temp data to show mess on index page
                TempData["successMess"] = "Category updated successfully";

                return RedirectToAction("Index", "Category");


            }
            return View();


        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }

            //search by id
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj=_db.Categories.Find(id);
            if ((obj == null)) 
            {
                return NotFound();

            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["successMess"] = "Category deleted successfully";

            return RedirectToAction("Index", "Category");
            


        }
    }
}
