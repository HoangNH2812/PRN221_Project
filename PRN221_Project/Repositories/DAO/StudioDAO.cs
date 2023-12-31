﻿using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class StudioDAO
    {
        //singleton
        private static StudioDAO instance = null;
        private static readonly object instanceLock = new object();
        private StudioDAO() { }
        public static StudioDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new StudioDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<Studio> GetAll()
        {
            IEnumerable<Studio> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.Studios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Studio GetByID(int id)
        {
            Studio studio;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                studio = DBContext.Studios.SingleOrDefault(i => i.StudioId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return studio;
        }
        public Studio GetByName(string name)
        {
            Studio studio;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                studio = DBContext.Studios.SingleOrDefault(i => i.Name == name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return studio;
        }

        public int AddNew(Studio Studio)
        {
             int id;
            Studio tmp;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                tmp = GetByName(Studio.Name);
               // Studio.StudioId = DBContext.Studios.OrderByDescending(i => i.StudioId).First().StudioId + 1;
               if (tmp != null)
                {
                    throw new Exception("name of studio has been existed");
                }
                else
                {
                    DBContext.Studios.Add(Studio);
                    DBContext.SaveChanges();
                    id = Studio.StudioId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }
        public void Update(Studio Studio)
        {
            try
            {
                Studio studio = GetByID(Studio.StudioId);
                if (studio != null)
                {
                    Studio check = GetByName(Studio.Name);
                    if (check != null && check.StudioId!=Studio.StudioId)
                    {
                        throw new Exception("name of studio has been existed");
                    }
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<Studio>(Studio).State = EntityState.Modified;
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
        public void Delete(Studio Studio)
        {
            try
            {
                Studio studio = GetByID(Studio.StudioId);
                if (studio != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Studios.Remove(studio);
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
