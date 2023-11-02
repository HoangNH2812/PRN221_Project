using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DAO
{
    internal class StyleDAO
    {
        //singleton
        private static StyleDAO instance = null;
        private static readonly object instanceLock = new object();
        private StyleDAO() { }
        public static StyleDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new StyleDAO();
                    }
                    return instance;
                }
            }
        }

        // --------------------------------------------------
        public IEnumerable<Style> GetAll()
        {
            IEnumerable<Style> list;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                list = DBContext.Styles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Style GetByID(int id)
        {
            Style style;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                style = DBContext.Styles.SingleOrDefault(i => i.StyleId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return style;
        }
        public Style GetByName(string name)
        {
            Style style;
            try
            {
                var DBContext = new ArtTattooLoverContext();
                style = DBContext.Styles.SingleOrDefault(i => i.StyleName.Equals(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return style;
        }
        public int AddNew(Style Style)
        {
            int id;
            try
            {
                var DBContext = new ArtTattooLoverContext();
           //     Style.StyleId = DBContext.Styles.OrderByDescending(i => i.StyleId).First().StyleId + 1;
                DBContext.Styles.Add(Style);
                DBContext.SaveChanges();
                id = Style.StyleId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }
        public void Update(Style Style)
        {
            try
            {
                Style style = GetByID(Style.StyleId);
                if (style != null)
                {
                    if (GetByName(Style.StyleName) != null)
                    {
                        throw new Exception("Name has existed");
                    }
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Entry<Style>(Style).State = EntityState.Modified;
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
        public void Delete(Style Style)
        {
            try
            {
                Style style = GetByID(Style.StyleId);
                if (style != null)
                {
                    var DBContext = new ArtTattooLoverContext();
                    DBContext.Styles.Remove(style);
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
