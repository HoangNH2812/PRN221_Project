using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ICertificateArtistRepository
    {
        public IEnumerable<CertificateArtist> GetAll();
        public IEnumerable<CertificateArtist> GetByArtistID(int id);
        public void Update(CertificateArtist CertificateArtist);
        public CertificateArtist GetCertificateArtist(int certId, int artistId);
        public CertificateArtist AddNew(CertificateArtist CertificateArtist);
        public void Delete(CertificateArtist CertificateArtist);
    }
}
