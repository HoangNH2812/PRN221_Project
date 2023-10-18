using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IStaffRepository
    {
        public IEnumerable<Staff> GetAll();
        public Staff GetByID(int id);
        public int AddNew(Staff Staff);
        public void Update(Staff Staff);
        public void Delete(Staff Staff);
    }
}
