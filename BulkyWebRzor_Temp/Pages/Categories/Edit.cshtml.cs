using BulkyWebRzor_Temp.Data;
using BulkyWebRzor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRzor_Temp.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Category category { get; set; }
        public IActionResult OnGet(int id)
        {
            category = _db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
