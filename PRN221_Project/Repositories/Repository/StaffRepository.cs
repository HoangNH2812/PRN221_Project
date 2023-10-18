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
    public class StaffRepository : IStaffRepository
    {
        public int AddNew(Staff Staff) => StaffDAO.Instance.AddNew(Staff);

        public void Delete(Staff Staff) => StaffDAO.Instance.Delete(Staff);

        public IEnumerable<Staff> GetAll() => StaffDAO.Instance.GetAll();

        public Staff GetByID(int id) => StaffDAO.Instance.GetByID(id);

        public void Update(Staff Staff) => StaffDAO.Instance.Update(Staff);
    }
}

