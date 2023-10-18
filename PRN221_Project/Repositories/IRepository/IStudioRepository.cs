using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IStudioRepository
    {
        public IEnumerable<Studio> GetAll();
        public Studio GetByID(int id);
        public int AddNew(Studio Studio);
        public void Update(Studio Studio);
        public void Delete(Studio Studio);
    }
}
