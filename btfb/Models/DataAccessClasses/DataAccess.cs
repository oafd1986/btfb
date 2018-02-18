using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using btfb.Models.DbAccessModel;

namespace btfb.Models.DataAccessClasses
{
    public class StatesDataAccess
    {
        public List<State> GetStates()
        {
            try
            {
                using (btfbEntities db = new btfbEntities())
                {
                   return db.States.OrderBy(x => x.Abbr).ToList();
                }
            }
            catch
            {
                return null;
            }
        }
    }
    public class MakesDataAccess
    {
        public List<Make> GetMakes()
        {
            try
            {
                using (btfbEntities db = new btfbEntities())
                {
                    
                   return  db.Makes.Include("Models").OrderBy(x=>x.Make1).ToList();
                }
            }
            catch
            {
                return null;
            }
        }
        public Make GetMake(int makeId)
        {
            try
            {
                using (btfbEntities db = new btfbEntities())
                {

                    return db.Makes.Include("Models").OrderBy(x => x.Make1).FirstOrDefault(x=>x.Id == makeId);
                }
            }
            catch
            {
                return null;
            }
        }
        public List<Model> GetModelsFromMake(int makeId)
        {
            try
            {
                using (btfbEntities db = new btfbEntities())
                {
                    return db.Models.Where(x => x.MakeId == makeId).ToList();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}