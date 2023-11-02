using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class CertificateDAO
    {
        //singleton
        private static CertificateDAO instance = null;
        private static readonly object instanceLock = new object();
        private CertificateDAO() { }
        public static CertificateDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CertificateDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<Certificate> GetAll()
        {
            IEnumerable<Certificate> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.Certificates;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Certificate GetByID(int id)
        {
            Certificate certificate;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                certificate = DBContext.Certificates.SingleOrDefault(i => i.CertificateId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return certificate;
        }
        public Certificate GetByName(string name)
        {
            Certificate certificate;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                certificate = DBContext.Certificates.SingleOrDefault(i => i.CertificateName.Equals(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return certificate;
        }

        public int AddNew(Certificate Certificate)
        {
            int id;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                if (GetByName(Certificate.CertificateName) != null)
                {
                    throw new Exception("Certificate name has already existed");
                }
               // Certificate.CertificateId = DBContext.Certificates.OrderByDescending(i => i.CertificateId).First().CertificateId + 1;
                DBContext.Certificates.Add(Certificate);
                DBContext.SaveChanges();
                id = Certificate.CertificateId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }
        public void Update(Certificate Certificate)
        {
            try
            {
                Certificate certificate = GetByID(Certificate.CertificateId);
                if (certificate != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<Certificate>(Certificate).State = EntityState.Modified;
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
        public void Delete(Certificate Certificate)
        {
            try
            {
                Certificate certificate = GetByID(Certificate.CertificateId);
                if (certificate != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Certificates.Remove(certificate);
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
