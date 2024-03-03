using BulkyWebRzor_Temp.Data;
using BulkyWebRzor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRzor_Temp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult OnGet(int? id)
        {
            if(id != null && id!=0)
            {
                Category = _db.Categories.Find(id);
                return OnPost();
            }
            return RedirectToPage(pageName: "Index");
        }
        public IActionResult OnPost()
        {
            if(Category != null)
            {
                _db.Categories.Remove(Category);
                _db.SaveChanges();
            }
            return RedirectToPage(pageName: "Index");
        }
    }
}
