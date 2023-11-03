using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.StudioManage
{
    public class DeleteModel : PageModel
    {
        private readonly IStudioRepository _studioRepository;

        public DeleteModel(IStudioRepository studioRepository)
        {
            _studioRepository = studioRepository;
        }

        [BindProperty]
        public Studio Studio { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studio = _studioRepository.GetByID(id.Value);

            if (studio == null)
            {
                return NotFound();
            }
            else
            {
                Studio = studio;
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studio = _studioRepository.GetByID(id.Value);

            if (studio != null)
            {
                Studio = studio;
                if (Studio.Status == 1)
                {
                    Studio.Status = 0;
                }
                else if (Studio.Status == 0)
                {
                    Studio.Status = 1;
                }
                _studioRepository.Update(Studio);
            }
            return RedirectToPage("./Index");
        }
    }
}
