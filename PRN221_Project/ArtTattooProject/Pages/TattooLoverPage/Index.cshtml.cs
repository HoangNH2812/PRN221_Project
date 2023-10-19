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
        public IndexModel(IServiceRepository serviceRepository, IConfiguration configuration)
        {
            _serviceRepository = serviceRepository;
            Configuration = configuration;
        }
        public string CurrentFilter { get; set; }
        public PaginatedList<Service> Service { get; set; } = default!;
        public IQueryable<Service> ServiceList { get; set; } = default!;

        public void OnGet(string searchString, int? pageIndex)
        {
            
            CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                ServiceList = _serviceRepository.GetByName(searchString).AsQueryable();
            } else
            {
                ServiceList = _serviceRepository.GetAll().AsQueryable();
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Service = PaginatedList<Service>.Create(
                ServiceList, pageIndex ?? 1, pageSize);
        }
    }
}
