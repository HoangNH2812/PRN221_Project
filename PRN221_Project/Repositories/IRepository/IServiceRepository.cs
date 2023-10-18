using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IServiceRepository
    {
        public IEnumerable<Service> GetAll();
        public Service GetByID(int id);
        public int AddNew(Service Service);
        public void Update(Service Service);
        public void Delete(Service Service);
    }
}
