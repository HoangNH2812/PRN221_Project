using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class ServiceDAO
    {
        //singleton
        private static ServiceDAO instance = null;
        private static readonly object instanceLock = new object();
        private ServiceDAO() { }
        public static ServiceDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<Service> GetAll()
        {
            IEnumerable<Service> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.Services.Include(i=>i.Artist);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Service GetByID(int id)
        {
            Service service;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                service = DBContext.Services.Include(i => i.Artist).SingleOrDefault(i => i.ServiceId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return service;
        }

        public IEnumerable<Service> GetByName(string name)
        {
            IEnumerable<Service> service;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                service = DBContext.Services.Include(i => i.Artist).Where(i => i.ServiceName.Contains(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return service;
        }
        public IEnumerable<Service> GetByArtist(int id)
        {
            IEnumerable<Service> service;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                service = DBContext.Services.Where(i => i.ArtistId==id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return service;
        }

        public int AddNew(Service Service)
        {
            int id;
            try
            {
                if (GetByName(Service.ServiceName) != null)
                {
                    throw new Exception("Service name has been existed");
                }
                var DBContext = new ArtTattooLoverContext();
              //  Service.ServiceId = DBContext.Services.OrderByDescending(i => i.ServiceId).First().ServiceId + 1;
                DBContext.Services.Add(Service);
                DBContext.SaveChanges();
                id = Service.ServiceId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }
        public void Update(Service Service)
        {
            try
            {
                Service service = GetByID(Service.ServiceId);
                if (service != null)
                {
                    if (GetByName(Service.ServiceName) != null)
                    {
                        throw new Exception("Service name has been existed");
                    }
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<Service>(Service).State = EntityState.Modified;
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
        public void Delete(Service Service)
        {
            try
            {
                Service service = GetByID(Service.ServiceId);
                if (service != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Services.Remove(service);
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
