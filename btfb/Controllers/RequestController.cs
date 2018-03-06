using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using btfb.Models.DbAccessModel;
using btfb.Models.DataAccessClasses;
using btfb.ViewModels;

namespace btfb.Controllers
{
    public class RequestController : Controller
    {
        private btfbEntities db = new btfbEntities();
        // GET: Request
        [AllowAnonymous]
        public ActionResult HomeCreate()
        {
            RequestViewModel request = new RequestViewModel();

            MakesDataAccess makesDA = new MakesDataAccess();           
            request.Makes = makesDA.GetMakesList();    
            request.Models = makesDA.GetModelsList();


            StatesDataAccess statesDA = new StatesDataAccess(); 
            request.States = statesDA.GetStatesList();
            

            Utils util = new Utils();
            request.Years = util.GetYearsList();
            return View(request);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> HomeCreate(RequestViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                Request request = new Request();
                request = rvm.Request;
                request.Make = int.Parse(rvm.SelectedMake);
                request.Model = int.Parse(rvm.SelectedModel);
                request.FromState = int.Parse(rvm.FromState);
                request.ToState = int.Parse(rvm.ToState);        
                        
                        
                db.Requests.Add(request);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Home",new { area = ""});
            }
            else
            {
                return View(rvm);
            }
            
        }
        [AllowAnonymous]
        public ActionResult Models()
        {
            MakesDataAccess makesDA = new MakesDataAccess();
            ViewBag.Model = makesDA.GetModelsList();
            return PartialView("_ModelPartial");
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Models(int? makeId)
        {
            MakesDataAccess makesDA = new MakesDataAccess();
            RequestViewModel request = new RequestViewModel();
            request.Models = makesDA.GetModelsList(makeId.GetValueOrDefault());
            return PartialView("_ModelPartial",request);
        }
       
    }
}