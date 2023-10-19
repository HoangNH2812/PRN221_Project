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

namespace ArtTattooProject.Pages.ArtistPage.DesignManage
{
    public class CreateModel : PageModel
    {
        private readonly ITattoosDesignRepository _tattoosDesignRepository;
        private readonly IStyleRepository _styleRepository;

        public CreateModel(ITattoosDesignRepository tattoosDesignRepository, IStyleRepository styleRepository)
        {
            _tattoosDesignRepository = tattoosDesignRepository;
            _styleRepository = styleRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["StyleId"] = new SelectList(_styleRepository.GetAll(), "StyleId", "StyleName");
            return Page();
        }

        [BindProperty]
        public TattoosDesign TattoosDesign { get; set; } = default!;
        public string Msg { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                TattoosDesign.ArtistId= HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId;
                _tattoosDesignRepository.AddNew(TattoosDesign);
            } catch (Exception ex)
            {
                Msg = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
