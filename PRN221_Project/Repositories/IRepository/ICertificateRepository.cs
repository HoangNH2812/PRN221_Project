using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ICertificateRepository
    {
        public IEnumerable<Certificate> GetAll();
        public Certificate GetByID(int id);
        public int AddNew(Certificate Certificate);
        public void Update(Certificate Certificate);
        public void Delete(Certificate Certificate);
    }
}
