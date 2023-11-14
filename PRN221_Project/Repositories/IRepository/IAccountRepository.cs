using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IAccountRepository
    {
        public Account Login(string username, string password);
        public Account GetById(int? artistId, int? tattooLoverId, int? staffId);
        public Account GetByUsername(string username);
        public void AddNew(Account Account);
        public void Update(Account Account);
        public IEnumerable<Account> GetAll();
        public void Delete(Account Account);
    }
}
