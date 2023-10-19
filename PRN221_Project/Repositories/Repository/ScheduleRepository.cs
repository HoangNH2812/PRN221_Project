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
    public class ScheduleRepository : IScheduleRepository
    {
        public int AddNew(Schedule Schedule) => ScheduleDAO.Instance.AddNew(Schedule);  

        public void Delete(Schedule Schedule) => ScheduleDAO.Instance.Delete(Schedule);

        public IEnumerable<Schedule> GetAll() => ScheduleDAO.Instance.GetAll();

        public Schedule GetByID(int id) => ScheduleDAO.Instance.GetByID(id);

        public void Update(Schedule Schedule) => ScheduleDAO.Instance.Update(Schedule);
        public IEnumerable<Schedule> GetSchedules(int artistID, int? status) => ScheduleDAO.Instance.GetSchedules(artistID, status);
    }
}
