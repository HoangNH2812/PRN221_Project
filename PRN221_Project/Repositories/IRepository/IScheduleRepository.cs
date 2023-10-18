using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IScheduleRepository
    {
        public IEnumerable<Schedule> GetAll();
        public Schedule GetByID(int id);
        public IEnumerable<Schedule> GetSchedules(int artistID, int status);
        public int AddNew(Schedule Schedule);
        public void Update(Schedule Schedule);
        public void Delete(Schedule Schedule);
    }
}
