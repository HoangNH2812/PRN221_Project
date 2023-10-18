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
    public class ArtistRepository : IArtistRepository
    {
        public int AddNew(Artist Artist) => ArtistDAO.Instance.AddNew(Artist);

        public void Delete(Artist Artist) => ArtistDAO.Instance.Delete(Artist);

        public IEnumerable<Artist> GetAll() => ArtistDAO.Instance.GetAll();

        public Artist GetByID(int id) => ArtistDAO.Instance.GetByID(id);

        public Artist GetByName(string name) => ArtistDAO.Instance.GetByName(name);

        public void Update(Artist Artist) => ArtistDAO.Instance.Update(Artist);
    }
}
