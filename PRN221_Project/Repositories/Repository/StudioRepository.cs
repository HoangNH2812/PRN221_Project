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
    public class StudioRepository : IStudioRepository
    {
        public int AddNew(Studio Studio) => StudioDAO.Instance.AddNew(Studio);

        public void Delete(Studio Studio) => StudioDAO.Instance.Delete(Studio);

        public IEnumerable<Studio> GetAll() => StudioDAO.Instance.GetAll();

        public Studio GetByID(int id) => StudioDAO.Instance.GetByID(id);

        public void Update(Studio Studio) => StudioDAO.Instance.Update(Studio);
    }
}
