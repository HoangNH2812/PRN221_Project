using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class AppointmentDAO
    {
        //singleton
        private static AppointmentDAO instance = null;
        private static readonly object instanceLock = new object();
        private AppointmentDAO() { }
        public static AppointmentDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AppointmentDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<Appointment> GetAll()
        {
            IEnumerable<Appointment> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.Appointments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public IEnumerable<Appointment> GetByTattooLover(int tattooLoverid)
        {
            IEnumerable<Appointment> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list= DBContext.Appointments.Where(i=>i.TattooLoverId == tattooLoverid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Appointment GetByID(int id)
        {
            Appointment appointment;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                appointment = DBContext.Appointments.SingleOrDefault(i => i.AppointmentId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return appointment;
        }

        public int AddNew(Appointment Appointment)
        {
            int id;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                /*Appointment.AppointmentId = DBContext.Appointments.OrderByDescending(i => i.AppointmentId).First().AppointmentId + 1;*/
                DBContext.Appointments.Add(Appointment);
                DBContext.SaveChanges();
                id = Appointment.AppointmentId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }
        public void Update(Appointment Appointment)
        {
            try
            {
                Appointment appointment = GetByID(Appointment.AppointmentId);
                if (appointment != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<Appointment>(Appointment).State = EntityState.Modified;
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
        public void Delete(Appointment Appointment)
        {
            try
            {
                Appointment appointment = GetByID(Appointment.AppointmentId);
                if (appointment != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Appointments.Remove(appointment);
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
