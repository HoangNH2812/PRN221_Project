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
    public class CertificateArtistRepository : ICertificateArtistRepository
    {
        public CertificateArtist AddNew(CertificateArtist CertificateArtist) => CertificateArtistDAO.Instance.AddNew(CertificateArtist);

        public void Delete(CertificateArtist CertificateArtist) => CertificateArtistDAO.Instance.Delete(CertificateArtist);

        public IEnumerable<CertificateArtist> GetAll() => CertificateArtistDAO.Instance.GetAll();

        public IEnumerable<CertificateArtist> GetByArtistID(int id) => CertificateArtistDAO.Instance.GetByArtistID(id);
    }
}
