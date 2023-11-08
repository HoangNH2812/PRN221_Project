using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.TattooLoverPage
{
    public class IndexModel : PageModel
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IConfiguration Configuration;
        private readonly ITattoosDesignRepository _taxtoosDesignRepository;
        public IndexModel(IServiceRepository serviceRepository, IConfiguration configuration, ITattoosDesignRepository tattoosDesignRepository)
        {
            _serviceRepository = serviceRepository;
            Configuration = configuration;
            _taxtoosDesignRepository = tattoosDesignRepository;
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
            if (!String.IsNullOrEmpty(searchString))
            {
                IEnumerable<Service> list = _serviceRepository.GetByName(searchString);
                foreach (var item in list)
                {
                    if (item.TattoosDesignId != null)
                    {
                        item.TattoosDesign = _taxtoosDesignRepository.GetByID(item.TattoosDesignId.Value);
                    }
                }
                ServiceList = list.AsQueryable();
            } else
            {
                IEnumerable<Service> list = _serviceRepository.GetAllAvailable();
                foreach (var item in list)
                {
                    if (item.TattoosDesignId != null)
                    {
                        int id = item.TattoosDesignId.Value;
                        item.TattoosDesign = _taxtoosDesignRepository.GetByID(id);
                    }
                }
                ServiceList = list.AsQueryable();
            }

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
