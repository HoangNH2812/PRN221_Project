using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ITattoosDesignRepository
    {
        public IEnumerable<TattoosDesign> GetAll();
        public TattoosDesign GetByID(int id);
        public int AddNew(TattoosDesign TattoosDesign);
        public void Update(TattoosDesign TattoosDesign);
        public void Delete(TattoosDesign TattoosDesign);
    }
}
