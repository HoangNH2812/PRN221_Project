using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DataAccess
{
    internal class ArtistDAO
    {
        //singleton
        private static ArtistDAO instance = null;
        private static readonly object instanceLock = new object();
        private ArtistDAO() { }
        public static ArtistDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ArtistDAO();
                    }
                    return instance;
                }
            }
        }

        //--------function-------
        public List<Artist> GetArtists()
        {
            List<Artist> ArtistsList = null;
            try
            {
                var context = new ArtTattooLoverContext();
                ArtistsList = context.Artists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ArtistsList;
        }
        public Artist GetArtist(int id)
        {
            Artist artists = null;
            try
            {
                var context = new ArtTattooLoverContext();
                artists = context.Artists.SingleOrDefault(a=>a.ArtistId==id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return artists;
        }

        public int Add(Artist item)
        {
            int result;
            try
            {
                var context = new ArtTattooLoverContext();
                context.Add(item);
                context.SaveChanges();
                result = item.ArtistId;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int Update(Artist item)
        {

            int result;
            try
            {
                var context = new ArtTattooLoverContext();
                context.Update(item);
                context.SaveChanges();
                result = item.ArtistId;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int Delete(Artist item)
        {
            int result;
            try
            {
                var context = new ArtTattooLoverContext();
                context.Remove(item);
                context.SaveChanges();
                result = item.ArtistId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
