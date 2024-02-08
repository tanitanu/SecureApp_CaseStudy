using DXC_Technology.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.SqlServer.Server;
using DXC_Technology.Model;

namespace DXC_Technology.Pages.FormView
{

    //Developed by Sravanthi
   // [BindProperties]
    public class IndexModel : PageModel
    {
        private FormDBContext _db;

        public void OnGet()
        {
        }
        
        public FormViews formView { get; set; }
        public IndexModel(FormDBContext dbcontext)
        {
            _db = dbcontext;
        }

       

        public async Task<IActionResult> OnPost(FormViews formview)
        {
            if (ModelState.IsValid)
            {
                await _db.AddAsync(formview);
                await _db.SaveChangesAsync();
                //Developed by Saravanan
                TempData["Success"] = "Thank You for contacting DXC! We will get back to you with a response Shortly.";
                return RedirectToPage("Index");
            }
            //TempData["Error"] = "Something went wrong! We will check and get back to you Shortly.";
            return Page();
        }
    }
}
