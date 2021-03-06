﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using btfb.Models.DbAccessModel;
using System.Web.Mvc;

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
        public List<SelectListItem> GetStatesList()
        {
            List<State> states = GetStates();
            List<SelectListItem> statesdropdownlist = new List<SelectListItem>();
            statesdropdownlist.Add(new SelectListItem { Text = "-State-", Value = "0" });

            if (states != null)
            {
                foreach (var state in states)
                {
                    statesdropdownlist.Add(new SelectListItem { Text = state.State1, Value = state.Id.ToString() });
                }
            }
            return statesdropdownlist;
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

                    return db.Makes.Include("Models").OrderBy(x => x.Make1).ToList();
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

                    return db.Makes.Include("Models").OrderBy(x => x.Make1).FirstOrDefault(x => x.Id == makeId);
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
        public List<SelectListItem> GetMakesList()
        {
            List<Make> makes = GetMakes();
            List<SelectListItem> makesdropdownlist = new List<SelectListItem>();
            makesdropdownlist.Add(new SelectListItem { Text = "-Make-", Value = "0" });

            if (makes != null)
            {
                foreach (var make in makes)
                {
                    makesdropdownlist.Add(new SelectListItem { Text = make.Make1, Value = make.Id.ToString() });
                }
            }
            return makesdropdownlist;
        }
        /// <summary>
        /// Will return an empty list of models.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetModelsList()
        {

            List<SelectListItem> modelsDropDownList = new List<SelectListItem>();
            modelsDropDownList.Add(new SelectListItem { Text = "-Model-", Value = "0" });
                       
            return modelsDropDownList;
        }
        /// <summary>
        /// Will return the models from the provided make
        /// </summary>
        /// <param name="makeId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetModelsList(int makeId)
        {
            try
            {
                List<Model> models = GetModelsFromMake(makeId);
                List<SelectListItem> modelsDropDownList = new List<SelectListItem>();
                modelsDropDownList.Add(new SelectListItem { Text = "-Model-", Value = "0" });
                if (models != null)
                {
                    foreach (var model in models)
                    {
                        modelsDropDownList.Add(new SelectListItem { Text = model.Model1, Value = model.id.ToString() });
                    }
                }
                return modelsDropDownList;
            }
            catch(Exception)
            {
                List<SelectListItem> modelsDropDownList = new List<SelectListItem>();
                modelsDropDownList.Add(new SelectListItem { Text = "-Model-", Value = "0" });

                return modelsDropDownList;
            }
        }
    }
    public class RequestDataAccess
    {

    }
    public class UsersDataAccess
    {

        public List<SelectListItem> GetNonAdminUsers()
        {

            var context = new ApplicationDbContext();
            var nonadmin = context.Roles.Single(b => b.Name == "Admins").Users;
            var allusers = context.Users.ToList();
            foreach(var nonadm in nonadmin)
            {
                allusers.RemoveAll(u=>u.Id == nonadm.UserId);
            }
            List<SelectListItem> usersdropdownlist = new List<SelectListItem>();
            usersdropdownlist.Add(new SelectListItem { Text = "-User-", Value = "0" });

            if (allusers != null)
            {
                foreach (var user in allusers)
                {
                    usersdropdownlist.Add(new SelectListItem
                    {
                        Text = user.Email,
                        Value = user.Id
                    });
                }
            }
            return usersdropdownlist;
          

        }
        public UsersDTO GetUser(string userId)
        {
            
                using (btfbEntities db = new btfbEntities())
                {
                    AspNetUser user = db.AspNetUsers.FirstOrDefault(u => u.Id == userId);
                    UsersDTO userDto = new UsersDTO();
                if (user!= null)
                {
                    userDto.firstName = user.FirstName;
                    userDto.lastName = user.LastName;
                    userDto.phone = user.PhoneNumber;
                    userDto.email = user.Email;
                }
                    
                return userDto;
                }
            
            
           
        }
        public static AspNetUser GetAspNetUser(string userId)
        {
            using (btfbEntities db = new btfbEntities())
            {
                return db.AspNetUsers.First(u => u.Id == userId);
            }
        }
    }
    public class Utils
    {
        public List<SelectListItem> GetYearsList()
        {
            List<SelectListItem> years = new List<SelectListItem>();
            years.Add(new SelectListItem { Text = "-Year-", Value = "0" });
            for (int i = DateTime.Now.Year + 2; i > 1885; i--)
            {
                years.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString().ToString() });
            }
            return years;
}
    }
    
}