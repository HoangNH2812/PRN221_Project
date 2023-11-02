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
    public class EditModel : PageModel
    {
        private readonly ITattoosDesignRepository _tattoosDesignRepository;
        private readonly IStyleRepository _styleRepository;

        public EditModel(ITattoosDesignRepository tattoosDesignRepository, IStyleRepository styleRepository)
        {
            _tattoosDesignRepository = tattoosDesignRepository;
            _styleRepository = styleRepository;
        }

        [BindProperty]
        public TattoosDesign TattoosDesign { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tattoosdesign = _tattoosDesignRepository.GetByID(id.Value);
            tattoosdesign.Style = _styleRepository.GetByID(tattoosdesign.StyleId.Value);
            if (tattoosdesign == null)
            {
                return NotFound();
            }
            TattoosDesign = tattoosdesign;
            ViewData["StyleId"] = new SelectList(_styleRepository.GetAll(), "StyleId", "StyleName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public  IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return OnGet(TattoosDesign.TattoosDesignId);
            }

            try
            {
                TattoosDesign.ArtistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId;
                _tattoosDesignRepository.Update(TattoosDesign);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TattoosDesignExists(TattoosDesign.TattoosDesignId))
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

        private bool TattoosDesignExists(int id)
        {
            return (_tattoosDesignRepository.GetByID(id) == null);
        }
    }
}
