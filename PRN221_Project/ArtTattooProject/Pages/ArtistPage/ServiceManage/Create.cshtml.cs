using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.ArtistPage.ServiceManage
{
    public class CreateModel : PageModel
    {
        private readonly IServiceRepository _serviceRepository;

        public CreateModel(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Service Service { get; set; } = default!;

        public string Msg { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public  IActionResult OnPost()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            int artistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
            try
            {
                Service.ArtistId = artistId;
                _serviceRepository.AddNew(Service);
            } catch (Exception ex)
            {
                Msg = ex.Message;
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
