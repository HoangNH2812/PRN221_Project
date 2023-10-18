using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.TattooLoverPage
{
    public class IndexModel : PageModel
    {
        IServiceRepository _serviceRepository;
        public IndexModel(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public IList<Service> Service { get; set; } = default!;
        public void OnGet()
        {
            Service = _serviceRepository.GetAll().ToList();
        }
    }
}
