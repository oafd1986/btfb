using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using btfb.Models;
using btfb.Models.DbAccessModel;
using btfb.Models.DataAccessClasses;

namespace btfb.Controllers
{
    public class ComboBoxController : Controller
    {
        // GET: ComboBox
        public ActionResult States(string comboId)
        {
            var model = new ComboBoxViewModels();
            model.ComboId = comboId;
            StatesDataAccess statesDA = new StatesDataAccess();
            List<State> states = statesDA.GetStates();
            List<SelectListItem> dropdownlist = new List<SelectListItem>();
            dropdownlist.Add(new SelectListItem { Text = "-State-", Value = "0" });
            foreach (var state in states)
            {
                dropdownlist.Add(new SelectListItem { Text = state.Abbr, Value = state.Id.ToString() });
            }
            model.SelectOptions = dropdownlist;
            return PartialView("_statePartial",model);
        }
        public ActionResult Makes(string comboId)
        {
            var model = new ComboBoxViewModels();
            model.ComboId = comboId;
            MakesDataAccess makesDA = new MakesDataAccess();
            List<Make> makes = makesDA.GetMakes();
            List<SelectListItem> dropdownlist = new List<SelectListItem>();
            dropdownlist.Add(new SelectListItem { Text = "-Make-", Value = "0" });
            foreach (var make in makes)
            {
                dropdownlist.Add(new SelectListItem { Text = make.Make1, Value = make.Id.ToString() });
            }
            model.SelectOptions = dropdownlist;
            return PartialView("_statePartial", model);
        }
        public ActionResult Models(string comboId, int? makeId)
        {
            var model = new ComboBoxViewModels();
            model.ComboId = comboId;
            if(makeId != null)
            {
                MakesDataAccess makesDa = new MakesDataAccess();
                Make make = 
            }
        }
    }
}