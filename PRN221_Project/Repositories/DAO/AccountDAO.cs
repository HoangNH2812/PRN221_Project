using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public Account GetByUsername(string username)
        {
            Account account = null;
            try
            {
                var DBcontext = new ArtTattooLoverContext();
                account = DBcontext.Accounts.FirstOrDefault(i => i.Username == username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public Account GetById(int? artistId, int? tattooLoverId, int? staffId)
        {
            Account account = null;
            try
            {
                var DBcontext = new ArtTattooLoverContext();
                if (artistId != null)
                {
                    account = DBcontext.Accounts.FirstOrDefault(i => i.ArtistId == artistId.Value);
                    return account;
                } else if (tattooLoverId != null)
                {
                    account = DBcontext.Accounts.FirstOrDefault(i => i.TattooLoverId == tattooLoverId.Value);
                    return account;
                } if (staffId != null)
                {
                    account = DBcontext.Accounts.FirstOrDefault(i => i.StaffId == staffId.Value);
                    return account;
                }
                else throw new Exception("no input for method");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
         
        }
        public void AddNew(Account Account)
        {
            try
            {
                var DBContext = new ArtTattooLoverContext();
                Account tmp = DBContext.Accounts.FirstOrDefault(i => i.Username == Account.Username);
                if (tmp == null)
                {
                    var expectedPasswordPattern = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

                    var isValidPassword = expectedPasswordPattern.IsMatch(Account.Password);
                    if (!isValidPassword)
                    {
                        throw new Exception("Password must have an upcase character, a number and one special character");
                    }
                    Account.Status = 1;
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
                Account account = GetByUsername(Account.Username);
                if (account != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    if (Account.Password != account.Password)
                    {
                        var expectedPasswordPattern = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
                        var isValidPassword = expectedPasswordPattern.IsMatch(Account.Password);
                        if (!isValidPassword)
                        {
                            throw new Exception("Password must have an upcase character, a number and one special character");
                        }
                    }

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
