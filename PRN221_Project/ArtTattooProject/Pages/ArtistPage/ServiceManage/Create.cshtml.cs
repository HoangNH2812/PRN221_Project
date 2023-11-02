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
using Repositories.Repository;

namespace ArtTattooProject.Pages.ArtistPage.ServiceManage
{
    public class CreateModel : PageModel
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ITattoosDesignRepository _taxtoosDesignRepository;
        public CreateModel(IServiceRepository serviceRepository, ITattoosDesignRepository taxtoosDesignRepository)
        {
            _serviceRepository = serviceRepository;
            _taxtoosDesignRepository = taxtoosDesignRepository;
        }

        public IActionResult OnGet()
        {
            int artistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
            List<TattoosDesign> tattoosDesignsList = _taxtoosDesignRepository.GetByArtist(artistId).ToList();
            tattoosDesignsList.Add(new TattoosDesign()
            {
                TattoosDesignId = 0,
                TattoosDesignName = "None",
            });
            tattoosDesignsList.OrderByDescending(t => t.TattoosDesignId).Reverse();
            List<SelectListItem> selectLists = new List<SelectListItem>();
            int i = 0;
            foreach (TattoosDesign tattoos in tattoosDesignsList)
            {
                selectLists.Insert(i, new SelectListItem()
                {
                    Value = tattoos.TattoosDesignId.ToString(),
                    Text = tattoos.TattoosDesignName
                }); ;
            }
            // ViewData["Design"] = new SelectList(tattoosDesignsList.AsEnumerable(), "TattoosDesignID", "TattoosDesignName");
            ViewData["Design"] = selectLists;
            return Page();
        }

        [BindProperty]
        public Service Service { get; set; } = default!;

        public string Msg { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return OnGet();
            }
            int artistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
            try
            {
                if (Service.TattoosDesignId == 0) { Service.TattoosDesignId = null; }
                Service.ArtistId = artistId;
                _serviceRepository.AddNew(Service);
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                return OnGet();
            }
            return RedirectToPage("./Index");
        }
    }
}
