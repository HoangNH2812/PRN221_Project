using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.StaffPage.ArtistManage
{
    public class IndexModel : PageModel
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IConfiguration Configuration;
        private readonly IAccountRepository _accountRepository;
        public IndexModel(IArtistRepository artistRepository, IStaffRepository staffRepository, IConfiguration configuration, IAccountRepository accountRepository)
        {
            _artistRepository = artistRepository;
            _staffRepository = staffRepository;
            Configuration = configuration;
            _accountRepository = accountRepository;
        }

        public IQueryable<ArtistMapper> ArtistList { get;set; } = default!;
        public PaginatedList<ArtistMapper> Artist { get;set; } = default!;

        public IActionResult OnGet(int? pageIndex)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else if (account.StaffId == null)
            {
                return RedirectToPage("/LoginPage");
            }
            int StaffId = HttpContext.Session.GetObjectFromJson<Account>("account").StaffId.Value;
            Staff staff = _staffRepository.GetByID(StaffId);

            IEnumerable<Artist> artistList = _artistRepository.GetByStudio(staff.StudioId.Value);
            List<ArtistMapper> list = new List<ArtistMapper>();

            foreach (Artist artist in artistList)
            {
                list.Add(new ArtistMapper(artist, _accountRepository.GetById(artist.ArtistId, null, null)));
            }
            ArtistList = list.AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Artist = PaginatedList<ArtistMapper>.Create(
                ArtistList, pageIndex ?? 1, pageSize);

            return Page();
        }
    }

    public class ArtistMapper
    {
        public Artist Artist { get; set; }
        public Account Account { get; set; }
        public ArtistMapper (Artist artist, Account account)
        {
            Artist = artist;
            Account = account;
        }
    }
}
