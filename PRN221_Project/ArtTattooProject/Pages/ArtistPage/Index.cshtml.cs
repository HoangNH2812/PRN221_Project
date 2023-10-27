using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArtTattooProject.Pages.ArtistPage
{
    public class IndexModel : PageModel
    {
        
        public IActionResult OnGet()
        {
            return RedirectToPage("./SchedulesManage/Index");
        }

     /*   public IActionResult OnPost() {
            return RedirectToAction("Index", "SchedulesController");
        }*/

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/LoginPage");
        }
    }    
}
