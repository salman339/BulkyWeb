using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area(areaName:"Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> categoryObject = _unitOfWork.Product.GetAll().ToList();

            return View(categoryObject);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            //if(obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The Display order cannot be exactly the same as Name.");
            //}
            //if (obj.Name !=null && obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value.");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
           return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            // We have 3 ways to get data
             Product? catergory = _unitOfWork.Product.Get(u=>u.Id == id);
             //Product? catergory = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Product? catergory = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();
            if ( catergory == null )
            {
                return NotFound();
            }

            return View(catergory);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // We have 3 ways to get data
            Product? product = _unitOfWork.Product.Get(u=>u.Id == id);
            //Product? catergory = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Product? catergory = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
