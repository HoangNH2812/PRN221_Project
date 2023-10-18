using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class TattooLoverDAO
    {
        //singleton
        private static TattooLoverDAO instance = null;
        private static readonly object instanceLock = new object();
        private TattooLoverDAO() { }
        public static TattooLoverDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TattooLoverDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<TattooLover> GetAll()
        {
            IEnumerable<TattooLover> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.TattooLovers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public TattooLover GetByID(int id)
        {
            TattooLover tattooLover;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                tattooLover = DBContext.TattooLovers.SingleOrDefault(i => i.TattooLoverId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tattooLover;
        }

        public int AddNew(TattooLover TattooLover)
        {
            int id;
            try
            {
                var DBContext = new ArtTattooLoverContext();
               // TattooLover.TattooLoverId = DBContext.TattooLovers.OrderByDescending(i => i.TattooLoverId).First().TattooLoverId + 1;
                DBContext.TattooLovers.Add(TattooLover);
                DBContext.SaveChanges();
                id = TattooLover.TattooLoverId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }
        public void Update(TattooLover TattooLover)
        {
            try
            {
                TattooLover tattooLover = GetByID(TattooLover.TattooLoverId);
                if (tattooLover != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<TattooLover>(TattooLover).State = EntityState.Modified;
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
        public void Delete(TattooLover TattooLover)
        {
            try
            {
                TattooLover tattooLover = GetByID(TattooLover.TattooLoverId);
                if (tattooLover != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.TattooLovers.Remove(TattooLover);
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
