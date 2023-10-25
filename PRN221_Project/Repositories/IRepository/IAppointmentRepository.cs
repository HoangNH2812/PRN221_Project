using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IAppointmentRepository
    {
        public IEnumerable<Appointment> GetAll();
        public IEnumerable<Appointment> GetByTattooLover(int tattooLoverid);
        public IEnumerable<Appointment> GetByStudio(int studioId);
        public Appointment GetByID(int id);
        public int AddNew(Appointment Appointment);
        public void Update(Appointment Appointment);
        public void Delete(Appointment Appointment);
    }
}
