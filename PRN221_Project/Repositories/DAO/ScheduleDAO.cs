using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class ScheduleDAO
    {
        //singleton
        private static ScheduleDAO instance = null;
        private static readonly object instanceLock = new object();
        private ScheduleDAO() { }
        public static ScheduleDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ScheduleDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<Schedule> GetAll()
        {
            IEnumerable<Schedule> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.Schedules;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Schedule GetByID(int id)
        {
            Schedule schedule;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                schedule = DBContext.Schedules.SingleOrDefault(i => i.ScheduleId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return schedule;
        }
        public IEnumerable<Schedule> GetSchedules(int artistID, int? status)
        {
            IEnumerable<Schedule> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                if (status == null)
                {
                    list = DBContext.Schedules.Where(i => i.ArtistId == artistID);
                } else
                {
                    list = DBContext.Schedules.Where(i => i.ArtistId == artistID && i.Status == status);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public int AddNew(Schedule Schedule)
        {
            int id;
            try
            {
                var DBContext = new ArtTattooLoverContext();
              //  Schedule.ScheduleId = DBContext.Schedules.OrderByDescending(i => i.ScheduleId).First().ScheduleId + 1;
                Schedule checkSchedule = DBContext.Schedules.SingleOrDefault(i => i.Time == Schedule.Time && i.ArtistId == Schedule.ArtistId);
                if (checkSchedule != null) throw new Exception("date already added in schedule");
                DBContext.Schedules.Add(Schedule);
                DBContext.SaveChanges();
                id = Schedule.ScheduleId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }
        public void Update(Schedule Schedule)
        {
            try
            {
                Schedule schedule = GetByID(Schedule.ScheduleId);
                if (schedule != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<Schedule>(Schedule).State = EntityState.Modified;
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
        public void Delete(Schedule Schedule)
        {
            try
            {
                Schedule schedule = GetByID(Schedule.ScheduleId);
                if (schedule != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Schedules.Remove(Schedule);
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
