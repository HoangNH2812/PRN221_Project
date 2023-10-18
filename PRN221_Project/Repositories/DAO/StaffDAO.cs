using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class StaffDAO
    {
        //singleton
        private static StaffDAO instance = null;
        private static readonly object instanceLock = new object();
        private StaffDAO() { }
        public static StaffDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new StaffDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<Staff> GetAll()
        {
            IEnumerable<Staff> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.Staff;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Staff GetByID(int id)
        {
            Staff staff;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                staff = DBContext.Staff.SingleOrDefault(i => i.StaffId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return staff;
        }

        public int AddNew(Staff Staff)
        {
            int id;
            try
            {
                var DBContext = new ArtTattooLoverContext();
               // Staff.StaffId = DBContext.Staff.OrderByDescending(i => i.StaffId).First().StaffId + 1;
                DBContext.Staff.Add(Staff);
                DBContext.SaveChanges();
                id = Staff.StaffId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }
        public void Update(Staff Staff)
        {
            try
            {
                Staff staff = GetByID(Staff.StaffId);
                if (staff != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<Staff>(Staff).State = EntityState.Modified;
                    DBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("ID not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(Staff Staff)
        {
            try
            {
                Staff staff = GetByID(Staff.StaffId);
                if (staff != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Staff.Remove(Staff);
                    DBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("ID not exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
