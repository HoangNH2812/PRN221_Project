﻿using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class TattoosDesignDAO
    {
        //singleton
        private static TattoosDesignDAO instance = null;
        private static readonly object instanceLock = new object();
        private TattoosDesignDAO() { }
        public static TattoosDesignDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TattoosDesignDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<TattoosDesign> GetAll()
        {
            IEnumerable<TattoosDesign> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.TattoosDesigns;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public TattoosDesign GetByID(int id)
        {
            TattoosDesign tattoosDesign;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                tattoosDesign = DBContext.TattoosDesigns.SingleOrDefault(i => i.TattoosDesignId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tattoosDesign;
        }

        public int AddNew(TattoosDesign TattoosDesign)
        {
            int id;
            try
            {
                var DBContext = new ArtTattooLoverContext();
              //  TattoosDesign.TattoosDesignId = DBContext.TattoosDesigns.OrderByDescending(i => i.TattoosDesignId).First().TattoosDesignId + 1;
                DBContext.TattoosDesigns.Add(TattoosDesign);
                DBContext.SaveChanges();
                id = TattoosDesign.TattoosDesignId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }
        public void Update(TattoosDesign TattoosDesign)
        {
            try
            {
                TattoosDesign tattoosDesign = GetByID(TattoosDesign.TattoosDesignId);
                if (tattoosDesign != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<TattoosDesign>(TattoosDesign).State = EntityState.Modified;
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
        public void Delete(TattoosDesign TattoosDesign)
        {
            try
            {
                TattoosDesign tattoosDesign = GetByID(TattoosDesign.TattoosDesignId);
                if (tattoosDesign != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.TattoosDesigns.Remove(TattoosDesign);
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
