using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ITattooLoverRepository
    {
        public IEnumerable<TattooLover> GetAll();
        public TattooLover GetByID(int id);
        public int AddNew(TattooLover TattooLover);
        public void Update(TattooLover TattooLover);
        public void Delete(TattooLover TattooLover);
    }
}
