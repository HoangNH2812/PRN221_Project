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
    public class CertificateRepository : ICertificateRepository
    {
        public int AddNew(Certificate Certificate) => CertificateDAO.Instance.AddNew(Certificate);

        public void Delete(Certificate Certificate) => CertificateDAO.Instance.Delete(Certificate);

        public IEnumerable<Certificate> GetAll() => CertificateDAO.Instance.GetAll();

        public Certificate GetByID(int id) => CertificateDAO.Instance.GetByID(id);

        public void Update(Certificate Certificate) => CertificateDAO.Instance.Update(Certificate);
    }
}
