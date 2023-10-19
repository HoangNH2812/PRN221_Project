using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class AccountDAO
    {
        //singleton
        private static AccountDAO instance = null;
        private static readonly object instanceLock = new object();
        private AccountDAO() { }
        public static AccountDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<Account> GetAll()
        {
            IEnumerable<Account> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.Accounts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Account Login(string username, string password)
        {
            Account account = null;
            try
            {
                var DBcontext = new ArtTattooLoverContext();
                account = DBcontext.Accounts.FirstOrDefault(i => i.Username == username && i.Password == password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public void AddNew(Account Account)
        {
            try
            {
                var DBContext = new ArtTattooLoverContext();
                Account tmp = DBContext.Accounts.FirstOrDefault(i => i.Username == Account.Username);
                if (tmp != null)
                {
                    DBContext.Accounts.Add(Account);
                    DBContext.SaveChanges();
                }
                else throw new Exception("username has been used");
                   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Account Account)
        {
            try
            {
                Account account = Login(Account.Username, Account.Password);
                if (account != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<Account>(Account).State = EntityState.Modified;
                    DBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("wrong password");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(Account Account)
        {
            try
            {
                Account account = Login(Account.Username, Account.Password);
                if (account != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Accounts.Remove(account);
                    DBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("wrong password");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
