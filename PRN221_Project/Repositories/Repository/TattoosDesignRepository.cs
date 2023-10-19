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
    public class TattoosDesignRepository : ITattoosDesignRepository
    {
        public int AddNew(TattoosDesign TattoosDesign) => TattoosDesignDAO.Instance.AddNew(TattoosDesign);

        public void Delete(TattoosDesign TattoosDesign) => TattoosDesignDAO.Instance.Delete(TattoosDesign);

        public IEnumerable<TattoosDesign> GetAll() => TattoosDesignDAO.Instance.GetAll();

        public TattoosDesign GetByID(int id) => TattoosDesignDAO.Instance.GetByID(id);

        public void Update(TattoosDesign TattoosDesign) => TattoosDesignDAO.Instance.Update(TattoosDesign);
        public IEnumerable<TattoosDesign> GetByArtist(int artistId) => TattoosDesignDAO.Instance.GetByArtist(artistId);
    }
}
