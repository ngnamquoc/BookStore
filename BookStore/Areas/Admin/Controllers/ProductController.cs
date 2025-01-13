using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //local variable
        private readonly IUnitOfWork _iunitOfWork;
        //constructor
        public ProductController(IUnitOfWork unitOfWork)
        {
            _iunitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _iunitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
       


            if (ModelState.IsValid)
            {
                _iunitOfWork.Product.Add(obj);
                _iunitOfWork.Save();
                TempData["successMess"] = "Product created successfully";
                return RedirectToAction("Index", "Product");


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
            Product? productFromDb = _iunitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            //server side validation
            if (ModelState.IsValid)
            {
                _iunitOfWork.Product.Update(obj);
                _iunitOfWork.Save();
                //temp data to show mess on index page
                TempData["successMess"] = "Product updated successfully";

                return RedirectToAction("Index", "Product");


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
            Product? ProductFromDb = _iunitOfWork.Product.Get(u => u.Id == id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _iunitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();

            }
            _iunitOfWork.Product.Remove(obj);
            _iunitOfWork.Save();
            TempData["successMess"] = "Product deleted successfully";

            return RedirectToAction("Index", "Product");



        }
    }
}
