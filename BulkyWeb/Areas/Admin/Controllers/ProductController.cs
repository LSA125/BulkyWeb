using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.Product.GetAll().ToList());
        }

        public IActionResult Delete(int? id)
        {
            Product obj = _unitOfWork.Product.Get(Product => Product.Id == id);
            if (obj == null) { return NotFound(); }
            return Delete(obj);
        }

        [HttpPost]
        public IActionResult Delete(Product obj)
        {
            if(!ModelState.IsValid) { return View(); }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if(id != null && id!=0) {
                productVM.Product = _unitOfWork.Product.Get(Product => Product.Id == id);
            }
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (!ModelState.IsValid) {
                obj.CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(obj); 
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productImage = Path.Combine(wwwRootPath,@"/Images/Product");
                if(!string.IsNullOrEmpty(obj.Product.CoverImageUrl))
                {
                    string oldImagePath = Path.Combine(wwwRootPath, obj.Product.CoverImageUrl.TrimStart('/'));
                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(productImage, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                obj.Product.CoverImageUrl = @"/Images/Product/" + fileName;
            }
            if(obj.Product.Id != 0)
            {
                _unitOfWork.Product.Update(obj.Product);
            }
            else
            {
                _unitOfWork.Product.Add(obj.Product);
            }
            _unitOfWork.Product.Add(obj.Product);
            _unitOfWork.Save();
            TempData["Success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
    }
}
