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
        public IEnumerable<Service> GetAllAvailable() => ServiceDAO.Instance.GetAllAvailable();
        public IEnumerable<Service> GetAll() => ServiceDAO.Instance.GetAll();
        public IEnumerable<Service> GetByName(string name) => ServiceDAO.Instance.GetByName(name);
        public Service GetByID(int id) => ServiceDAO.Instance.GetByID(id);
        public IEnumerable<Service> GetByArtist(int id) => ServiceDAO.Instance.GetByArtist(id);

        public void Update(Service Service) => ServiceDAO.Instance.Update(Service);
    }
}
