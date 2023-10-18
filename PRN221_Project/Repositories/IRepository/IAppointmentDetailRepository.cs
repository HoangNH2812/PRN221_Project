using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IAppointmentDetailRepository
    {
        public IEnumerable<AppointmentDetail> GetAll();
        public AppointmentDetail GetByID(int id);
        public IEnumerable<AppointmentDetail> GetByAppointmentID(int AppointmentID);
        public int AddNew(AppointmentDetail AppointmentDetail);
        public void Update(AppointmentDetail AppointmentDetail);
        public void Delete(AppointmentDetail AppointmentDetail);

    }
}
