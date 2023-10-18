using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IStyleRepository
    {
        public IEnumerable<Style> GetAll();
        public Style GetByID(int id);
        public int AddNew(Style Style);
        public void Update(Style Style);
        public void Delete(Style Style);
    }
}
