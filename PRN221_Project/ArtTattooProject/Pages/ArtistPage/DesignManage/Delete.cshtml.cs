using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.ArtistPage.DesignManage
{
    public class DeleteModel : PageModel
    {
        private readonly ITattoosDesignRepository _tattoosDesignRepository;
        private readonly IStyleRepository _styleRepository;

        public DeleteModel(ITattoosDesignRepository tattoosDesignRepository, IStyleRepository styleRepository)
        {
            _tattoosDesignRepository = tattoosDesignRepository;
            _styleRepository = styleRepository;
        }

        [BindProperty]
        public TattoosDesign TattoosDesign { get; set; } = default!;
        public string Msg { get; set; }
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

            var tattoosdesign = _tattoosDesignRepository.GetByID(id.Value);
            tattoosdesign.Style = _styleRepository.GetByID(tattoosdesign.StyleId.Value);

            if (tattoosdesign == null)
            {
                return NotFound();
            }
            else
            {
                TattoosDesign = tattoosdesign;
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var tattoosdesign = _tattoosDesignRepository.GetByID(id.Value);

                if (tattoosdesign != null)
                {
                    TattoosDesign = tattoosdesign;
                    _tattoosDesignRepository.Delete(TattoosDesign);
                }
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                return OnGet(id);
            }


            return RedirectToPage("./Index");
        }
    }
}
