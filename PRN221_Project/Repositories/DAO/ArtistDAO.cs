using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
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


        // --------------------------------------------------
        public IEnumerable<Artist> GetAll()
        {
            IEnumerable<Artist> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.Artists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public IEnumerable<Artist> GetByStudio(int studioId)
        {
            IEnumerable<Artist> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.Artists.Where(i=>i.StudioId==studioId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Artist GetByID(int id)
        {
            Artist artist;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                artist = DBContext.Artists.SingleOrDefault(i => i.ArtistId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return artist;
        }

        public Artist GetByName(string name)
        {
            Artist artist;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                artist = DBContext.Artists.SingleOrDefault(i => i.Fullname == name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return artist;
        }

        public int AddNew(Artist Artist)
        {
            int id;
            try
            {
                var DBContext = new ArtTattooLoverContext();
               // Artist.ArtistId = DBContext.Artists.OrderByDescending(i => i.ArtistId).First().ArtistId+1;
                DBContext.Artists.Add(Artist);
                DBContext.SaveChanges();
                id = Artist.ArtistId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }
        public void Update(Artist Artist)
        {
            try
            {
                Artist cus = GetByID(Artist.ArtistId);
                if (cus != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<Artist>(Artist).State = EntityState.Modified;
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
        public void Delete(Artist Artist)
        {
            try
            {
                Artist artist = GetByID(Artist.ArtistId);
                if (artist != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Artists.Remove(artist);
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
