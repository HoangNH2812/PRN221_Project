using Repositories.DAO;
using Repositories.IRepository;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public void AddNew(Account Account) => AccountDAO.Instance.AddNew(Account);

        public void Delete(Account Account) => AccountDAO.Instance.Delete(Account);

        public Account Login(string username, string password) => AccountDAO.Instance.Login(username, password);

        public void Update(Account Account) => AccountDAO.Instance.Update(Account);
    }
}
