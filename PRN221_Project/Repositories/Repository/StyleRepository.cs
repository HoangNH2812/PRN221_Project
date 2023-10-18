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
    public class StyleRepository : IStyleRepository
    {
        public int AddNew(Style Style) => StyleDAO.Instance.AddNew(Style);

        public void Delete(Style Style) => StyleDAO.Instance.Delete(Style);

        public IEnumerable<Style> GetAll() => StyleDAO.Instance.GetAll();

        public Style GetByID(int id) => StyleDAO.Instance.GetByID(id);

        public void Update(Style Style) => StyleDAO.Instance.Update(Style);
    }
}
