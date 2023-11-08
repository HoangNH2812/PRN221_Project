using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.ArtistPage.ServiceManage
{
    public class EditModel : PageModel
    {
        private readonly IServiceRepository _serviceRepository;

        public EditModel(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [BindProperty]
        public Service Service { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else if (account.ArtistId == null)
            {
                return RedirectToPage("/LoginPage");
            }
            if (id == null)
            {
                return NotFound();
            }
            var service = _serviceRepository.GetByID(id.Value);
            if (service == null)
            {
                return NotFound();
            }
            Service = service;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Service.ArtistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
                _serviceRepository.Update(Service);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(Service.ServiceId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ServiceExists(int id)
        {
          return (_serviceRepository.GetByID(id)==null);
        }
    }
}
