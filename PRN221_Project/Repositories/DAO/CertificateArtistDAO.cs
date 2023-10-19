using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class CertificateArtistDAO
    {
        //singleton
        private static CertificateArtistDAO instance = null;
        private static readonly object instanceLock = new object();
        private CertificateArtistDAO() { }
        public static CertificateArtistDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CertificateArtistDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<CertificateArtist> GetAll()
        {
            IEnumerable<CertificateArtist> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.CertificateArtists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public IEnumerable<CertificateArtist> GetByArtistID(int id)
        {
            IEnumerable<CertificateArtist> certificateArtist;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                certificateArtist = DBContext.CertificateArtists.Include(i=>i.Certificate).Where(i => i.ArtistId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return certificateArtist;
        }

        public CertificateArtist GetCertificateArtist(int certId, int artistId)
        {
            CertificateArtist certificateArtist;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                certificateArtist = DBContext.CertificateArtists
                    .Include(i => i.Certificate)
                    .SingleOrDefault(i => i.ArtistId == artistId && i.CertificateId == certId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return certificateArtist;
        }
        public CertificateArtist AddNew(CertificateArtist CertificateArtist)
        {
            CertificateArtist tmp;
            try
            {
                var DBContext = new ArtTattooLoverContext();

                tmp = DBContext.CertificateArtists.FirstOrDefault(i => i.CertificateId == CertificateArtist.CertificateId && i.ArtistId == CertificateArtist.ArtistId);
                if (tmp != null)
                {
                    throw new Exception("you have already add this certificate, please update to change certificate data");
                }
                else
                {
                    DBContext.CertificateArtists.Add(CertificateArtist);
                    DBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tmp;
        }
        public void Update(CertificateArtist CertificateArtist)
        {
            try
            {
                CertificateArtist certificateArtist = GetCertificateArtist(CertificateArtist.CertificateId.Value, CertificateArtist.ArtistId.Value);
                if (certificateArtist != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<CertificateArtist>(CertificateArtist).State = EntityState.Modified;
                    DBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(CertificateArtist CertificateArtist)
        {
            try
            {
                var DBContext = new ArtTattooLoverContext();
                CertificateArtist certificateArtist = DBContext.CertificateArtists.FirstOrDefault(i => i.CertificateId == CertificateArtist.CertificateId && i.ArtistId == CertificateArtist.ArtistId);
                if (certificateArtist != null)
                {
                    DBContext.CertificateArtists.Remove(certificateArtist);
                    DBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("ID not exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
