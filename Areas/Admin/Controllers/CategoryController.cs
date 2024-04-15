using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area(areaName:"Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Catergory> categoryObject = _unitOfWork.Category.GetAll().ToList();

            return View(categoryObject);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Catergory obj)
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
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
             Catergory? catergory = _unitOfWork.Category.Get(u=>u.Id == id);
             //Catergory? catergory = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Catergory? catergory = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();
            if ( catergory == null )
            {
                return NotFound();
            }

            return View(catergory);
        }
        [HttpPost]
        public IActionResult Edit(Catergory obj)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
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
            Catergory? catergory = _unitOfWork.Category.Get(u=>u.Id == id);
            //Catergory? catergory = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Catergory? catergory = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();
            if (catergory == null)
            {
                return NotFound();
            }

            return View(catergory);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Catergory obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
