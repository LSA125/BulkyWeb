using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork db)
        {

            _unitOfWork = db;

        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _unitOfWork.Category.Get(T => T.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category edited successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            return DeletePost(id);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _unitOfWork.Category.Get(T => T.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
