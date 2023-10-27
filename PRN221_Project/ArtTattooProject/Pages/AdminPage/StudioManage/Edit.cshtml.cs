using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.StudioManage
{
    public class EditModel : PageModel
    {
        private readonly IStudioRepository _studioRepository;
        public EditModel(IStudioRepository studioRepository)
        {
            _studioRepository = studioRepository;
        }

        [BindProperty]
        public Studio Studio { get; set; } = default!;
        public string Msg { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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
            Studio = studio;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
               _studioRepository.Update(Studio);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!StudioExists(Studio.StudioId))
                {
                    return NotFound();
                }
                else
                {
                    Msg = ex.Message;
                    return Page();
                }
            } catch (Exception ex)
            {
                Msg = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private bool StudioExists(int id)
        {
          return _studioRepository.GetByID(id)==null?true:false;
        }
    }
}
