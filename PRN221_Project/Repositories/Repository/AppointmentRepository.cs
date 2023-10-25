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
    public class AppointmentRepository : IAppointmentRepository
    {
        public int AddNew(Appointment Appointment) => AppointmentDAO.Instance.AddNew(Appointment);

        public void Delete(Appointment Appointment) => AppointmentDAO.Instance.Delete(Appointment);

        public IEnumerable<Appointment> GetAll() => AppointmentDAO.Instance.GetAll();
        public IEnumerable<Appointment> GetByStudio(int studioId) => AppointmentDAO.Instance.GetByStudio(studioId);

        public Appointment GetByID(int id) => AppointmentDAO.Instance.GetByID(id);

        public IEnumerable<Appointment> GetByTattooLover(int tattooLoverid) => AppointmentDAO.Instance.GetByTattooLover(tattooLoverid);     

        public void Update(Appointment Appointment) => AppointmentDAO.Instance.Update(Appointment);
    }
}
