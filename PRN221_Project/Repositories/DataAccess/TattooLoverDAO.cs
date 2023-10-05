using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DataAccess
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

        //--------function-------
        public List<TattooLover> GetTattooLovers()
        {
            List<TattooLover> tattooLoversList = null;
            try
            {
                var context = new ArtTattooLoverContext();
                tattooLoversList = context.TattooLovers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tattooLoversList;
        }

        public TattooLover GetTattooLover(int id)
        {
            TattooLover tattooLover = null;
            try
            {
                var context = new ArtTattooLoverContext();
                tattooLover = context.TattooLovers.SingleOrDefault(a => a.TattooLoverId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tattooLover;
        }

        public int Add(TattooLover item)
        {
            int result;
            try
            {
                var context = new ArtTattooLoverContext();
                context.Add(item);
                context.SaveChanges();
                result = item.TattooLoverId;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int Update(TattooLover item)
        {

            int result;
            try
            {
                var context = new ArtTattooLoverContext();
                context.Update(item);
                context.SaveChanges();
                result = item.TattooLoverId;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int Delete(TattooLover item)
        {
            int result;
            try
            {
                var context = new ArtTattooLoverContext();
                context.Remove(item);
                context.SaveChanges();
                result = item.TattooLoverId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
