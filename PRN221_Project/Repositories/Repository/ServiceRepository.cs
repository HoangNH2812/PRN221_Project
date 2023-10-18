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
    public class ServiceRepository : IServiceRepository
    {
        public int AddNew(Service Service) => ServiceDAO.Instance.AddNew(Service);

        public void Delete(Service Service) => ServiceDAO.Instance.Delete(Service);

        public IEnumerable<Service> GetAll() => ServiceDAO.Instance.GetAll();

        public Service GetByID(int id) => ServiceDAO.Instance.GetByID(id);

        public void Update(Service Service) => ServiceDAO.Instance.Update(Service);
    }
}
