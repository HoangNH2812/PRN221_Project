using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DataAccess
{
    internal class LoginDAO
    {
        //singleton
        private static LoginDAO instance = null;
        private static readonly object instanceLock = new object();
        private LoginDAO() { }
        public static LoginDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LoginDAO();
                    }
                    return instance;
                }
            }
        }

        //--------function-------
       
        public Login GetLogin(string username, string password)
        {
            Login Logins = null;
            try
            {
                var context = new ArtTattooLoverContext();
                Logins = context.Logins.SingleOrDefault(a => a.Username == username && a.Password==password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Logins;
        }

        public int? Add(Login item)
        {
            int? result = 0;
            try
            {
                var context = new ArtTattooLoverContext();
                context.Add(item);
                context.SaveChanges();
                if (item.StaffId!= null)
                {
                    result = item.StaffId;
                } else if (item.ArtistId!=null)
                {
                    result = item.ArtistId;
                } else if (item.TattooLoverId!=null)
                {
                    result = item.TattooLoverId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int? Update(Login item)
        {

            int? result = 0;
            try
            {
                var context = new ArtTattooLoverContext();
                context.Update(item);
                context.SaveChanges();
                if (item.StaffId != null)
                {
                    result = item.StaffId;
                }
                else if (item.ArtistId != null)
                {
                    result = item.ArtistId;
                }
                else if (item.TattooLoverId != null)
                {
                    result = item.TattooLoverId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int? Delete(Login item)
        {
            int? result = 0;
            try
            {
                var context = new ArtTattooLoverContext();
                context.Remove(item);
                context.SaveChanges();
                if (item.StaffId != null)
                {
                    result = item.StaffId;
                }
                else if (item.ArtistId != null)
                {
                    result = item.ArtistId;
                }
                else if (item.TattooLoverId != null)
                {
                    result = item.TattooLoverId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
