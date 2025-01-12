using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        //local variable
        private readonly IUnitOfWork _iunitOfWork;
        //constructor
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _iunitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _iunitOfWork.Category.GetAll().ToList();
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
                _iunitOfWork.Category.Add(obj);
                _iunitOfWork.Save();
                TempData["successMess"] = "Category created successfully";
                return RedirectToAction("Index", "Category");


            }
            return View();


        }

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }

            //search by id
            Category? categoryFromDb= _iunitOfWork.Category.Get(u=>u.Id==id);
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
                _iunitOfWork.Category.Update(obj);
                _iunitOfWork.Save();
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
            Category? categoryFromDb = _iunitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj= _iunitOfWork.Category.Get(u => u.Id == id);
            if ((obj == null)) 
            {
                return NotFound();

            }
            _iunitOfWork.Category.Remove(obj);
            _iunitOfWork.Save();
            TempData["successMess"] = "Category deleted successfully";

            return RedirectToAction("Index", "Category");
            


        }
    }
}
