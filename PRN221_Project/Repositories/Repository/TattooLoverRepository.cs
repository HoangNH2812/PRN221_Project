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
    public class TattooLoverRepository : ITattooLoverRepository
    {
        public int AddNew(TattooLover TattooLover) => TattooLoverDAO.Instance.AddNew(TattooLover);

        public void Delete(TattooLover TattooLover) => TattooLoverDAO.Instance.Delete(TattooLover);

        public IEnumerable<TattooLover> GetAll() => TattooLoverDAO.Instance.GetAll();

        public TattooLover GetByID(int id) => TattooLoverDAO.Instance.GetByID(id);

        public void Update(TattooLover TattooLover) => TattooLoverDAO.Instance.Update(TattooLover);
    }
}
