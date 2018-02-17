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
}