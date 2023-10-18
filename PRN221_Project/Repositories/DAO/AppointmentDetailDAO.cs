using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class AppointmentDetailDAO
    {
        //singleton
        private static AppointmentDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private AppointmentDetailDAO() { }
        public static AppointmentDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AppointmentDetailDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<AppointmentDetail> GetAll()
        {
            IEnumerable<AppointmentDetail> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.AppointmentDetails.Include(i => i.Service);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public IEnumerable<AppointmentDetail> GetByAppointmentID(int AppointmentID)
        {
            IEnumerable<AppointmentDetail> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.AppointmentDetails.Where(i=> i.AppointmentId==AppointmentID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public AppointmentDetail GetByID(int id)
        {
            AppointmentDetail appointmentDetail;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                appointmentDetail = DBContext.AppointmentDetails.SingleOrDefault(i => i.AppointmentDetailId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return appointmentDetail;
        }

        public int AddNew(AppointmentDetail AppointmentDetail)
        {
            int id;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                //AppointmentDetail.AppointmentDetailId = DBContext.AppointmentDetails.OrderByDescending(i => i.AppointmentDetailId).First().AppointmentDetailId + 1;
                DBContext.AppointmentDetails.Add(AppointmentDetail);
                DBContext.SaveChanges();
                id = AppointmentDetail.AppointmentDetailId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }
        public void Update(AppointmentDetail AppointmentDetail)
        {
            try
            {
                AppointmentDetail appointmentDetail = GetByID(AppointmentDetail.AppointmentDetailId);
                if (appointmentDetail != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<AppointmentDetail>(AppointmentDetail).State = EntityState.Modified;
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
        public void Delete(AppointmentDetail AppointmentDetail)
        {
            try
            {
                AppointmentDetail appointmentDetail = GetByID(AppointmentDetail.AppointmentDetailId);
                if (appointmentDetail != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.AppointmentDetails.Remove(AppointmentDetail);
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
