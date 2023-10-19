﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.ArtistPage.ServiceManage
{
    public class DeleteModel : PageModel
    {
        private readonly IServiceRepository _serviceRepository;

        public DeleteModel(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [BindProperty]
      public Service Service { get; set; } = default!;

        public  IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = _serviceRepository.GetByID(id.Value);

            if (service == null)
            {
                return NotFound();
            }
            else 
            {
                Service = service;
            }
            return Page();
        }

        public  IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var service = _serviceRepository.GetByID(id.Value);

            if (service != null)
            {
                _serviceRepository.Delete(service);
            }

            return RedirectToPage("./Index");
        }
    }
}