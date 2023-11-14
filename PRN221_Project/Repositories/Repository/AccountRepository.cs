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
        public Account GetByUsername(string username) => AccountDAO.Instance.GetByUsername(username);
        public void Delete(Account Account) => AccountDAO.Instance.Delete(Account);
        public IEnumerable<Account> GetAll() => AccountDAO.Instance.GetAll();
        public Account Login(string username, string password) => AccountDAO.Instance.Login(username, password);
        public Account GetById(int? artistId, int? tattooLoverId, int? staffId) => AccountDAO.Instance.GetById(artistId, tattooLoverId, staffId);
        public void Update(Account Account) => AccountDAO.Instance.Update(Account);
    }
}
