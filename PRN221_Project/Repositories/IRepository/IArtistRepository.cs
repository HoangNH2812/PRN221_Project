using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IArtistRepository
    {
        public IEnumerable<Artist> GetAll();
        public Artist GetByID(int id);
        public Artist GetByName(string name);
        public int AddNew(Artist Artist);
        public void Update(Artist Artist);
        public void Delete(Artist Artist);
    }
}
