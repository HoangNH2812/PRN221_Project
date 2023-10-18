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
    public class AppointmentDetailRepository : IAppointmentDetailRepository
    {
        public int AddNew(AppointmentDetail AppointmentDetail) => AppointmentDetailDAO.Instance.AddNew(AppointmentDetail);

        public void Delete(AppointmentDetail AppointmentDetail) => AppointmentDetailDAO.Instance.Delete(AppointmentDetail);

        public IEnumerable<AppointmentDetail> GetAll() => AppointmentDetailDAO.Instance.GetAll();
        public IEnumerable<AppointmentDetail> GetByAppointmentID(int AppointmentID) => AppointmentDetailDAO.Instance.GetByAppointmentID(AppointmentID);
        public AppointmentDetail GetByID(int id) => AppointmentDetailDAO.Instance.GetByID(id);

        public void Update(AppointmentDetail AppointmentDetail) => AppointmentDetailDAO.Instance.Update(AppointmentDetail);
    }
}
