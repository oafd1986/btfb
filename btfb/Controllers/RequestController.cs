using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using btfb.Models.DbAccessModel;
using btfb.Models.DataAccessClasses;
using btfb.ViewModels;

namespace btfb.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        [AllowAnonymous]
        public ActionResult HomeCreate()
        {
            RequestViewModel model = new RequestViewModel();
            MakesDataAccess makesDA = new MakesDataAccess();
            List<Make> makes = new List<Make>();
            makes.Add(new Make { Id = 0, Make1 = "-Make-" });
            makes.AddRange(makesDA.GetMakes());

            StatesDataAccess statesDA = new StatesDataAccess();
            List<State> states = new List<State>();
            states.Add(new State { Id = 0, State1 = "-State-" });
            states.AddRange(statesDA.GetStates());

            List<Model> models = new List<Model>();
            models.Add(new Model { id = 0, Model1 = "-Model-" });
            ViewBag.Model = new SelectList(models, "id", "Model1");


            ViewBag.Make = new SelectList(makes, "Id", "Make1");
            ViewBag.Model = new SelectList(models, "id", "Model1");
            ViewBag.FromState = new SelectList(states, "Id", "State1");
            ViewBag.ToState = new SelectList(states, "Id", "State1");
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult HomeCreate(RequestViewModel rvm)
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Models()
        {
            List<Model> models = new List<Model>();
            models.Add(new Model { id = 0, Model1 = "-Model-" });
            ViewBag.Model = new SelectList(models, "id", "Model1");
            return PartialView("_ModelPartial");
        }
    }
}