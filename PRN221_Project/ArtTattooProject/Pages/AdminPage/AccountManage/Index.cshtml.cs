using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.AccountManage
{
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration Configuration;

        public IndexModel(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            Configuration = configuration;
        }

        public IQueryable<Account> AccountList { get;set; } = default!;
        public PaginatedList<Account> Account { get;set; } = default!;

        public void OnGet(int? pageIndex)
        {
            AccountList = _accountRepository.GetAll().AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Account = PaginatedList<Account>.Create(
                AccountList, pageIndex ?? 1, pageSize);
        }
    }
}
