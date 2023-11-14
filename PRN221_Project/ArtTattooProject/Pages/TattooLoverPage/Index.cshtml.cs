using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;
using System.Collections.Generic;

namespace ArtTattooProject.Pages.TattooLoverPage
{
    public class IndexModel : PageModel
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IConfiguration Configuration;
        private readonly ITattoosDesignRepository _taxtoosDesignRepository;
        private readonly IAccountRepository _accountRepository;
        public IndexModel(IServiceRepository serviceRepository, IConfiguration configuration, ITattoosDesignRepository tattoosDesignRepository, IAccountRepository accountRepository)
        {
            _serviceRepository = serviceRepository;
            Configuration = configuration;
            _taxtoosDesignRepository = tattoosDesignRepository;
            _accountRepository = accountRepository;
        }
        public string CurrentFilter { get; set; }
        public PaginatedList<Service> Service { get; set; } = default!;
        public IQueryable<Service> ServiceList { get; set; } = default!;

        public IActionResult OnGet(string searchString, int? pageIndex)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else if (account.TattooLoverId == null)
            {
                return RedirectToPage("/LoginPage");
            }
            CurrentFilter = searchString;
            List<Service> list;
            if (!String.IsNullOrEmpty(searchString))
            {
                list = _serviceRepository.GetByName(searchString).ToList();
                foreach (var item in list)
                {
                    if (item.TattoosDesignId != null)
                    {
                        item.TattoosDesign = _taxtoosDesignRepository.GetByID(item.TattoosDesignId.Value);
                    }
                }
            } else
            {
              list = _serviceRepository.GetAllAvailable().ToList();
                foreach (var item in list)
                {
                    if (item.TattoosDesignId != null)
                    {
                        int id = item.TattoosDesignId.Value;
                        item.TattoosDesign = _taxtoosDesignRepository.GetByID(id);
                    }
                }
            }
            for (int i = 0; i<list.Count;i++)
            {
                int status = _accountRepository.GetById(list[i].ArtistId,null,null).Status;
                if (status == 0) {
                    list.Remove(list[i]);
                    i--;
                }
            }
            ServiceList = list.AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Service = PaginatedList<Service>.Create(
                ServiceList, pageIndex ?? 1, pageSize);
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/LoginPage");
        }
    }
}
