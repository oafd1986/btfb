using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using btfb.Models.DbAccessModel;
using btfb.Models.DataAccessClasses;
using btfb.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using btfb.Models;
using btfb.Helpers;
using System.Text;
using System.Net;
using System.Data.Entity;

namespace btfb.Controllers
{
    public class RequestController : Controller
    {

        private btfbEntities db = new btfbEntities();

        [AllowAnonymous]
        public ActionResult HomeCreate()
        {
            RequestViewModel request = new RequestViewModel();
            request.Request = new Request();

            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                request.Request.FirstName = user.FirstName;
                request.Request.LastName = user.LastName;
                request.Request.phone = user.PhoneNumber;
                request.Request.email = user.Email;
            }

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
        public async Task<ActionResult> HomeCreate(RequestViewModel rvm)
        {

            if (ModelState.IsValid)
            {
                Request request = new Request();
                request = rvm.Request;

                request.Make = int.Parse(rvm.SelectedMake);
                request.Model = int.Parse(rvm.SelectedModel);
                request.FromState = int.Parse(rvm.FromState);
                request.ToState = int.Parse(rvm.ToState);
                request.Year = rvm.SelectedYear;

                db.Requests.Add(request);
                await db.SaveChangesAsync();
                //notify business
                Email mail = new Email();
                StringBuilder body = new StringBuilder();
                mail.mailSubject = "New quote request";
                body.Append("You have a new quote request.\n\n");
                body.Append("From:" + request.FirstName + " " + request.LastName + "\n");
                body.Append("Phone: " + request.phone + "\n");
                body.Append("Email: " + request.email + "\n");
                // body.Append("Request Id: "+request.RequestId+"\n");

                mail.msgbody = body;
                mail.SendEmail();
                //notify client that his request is being proccessed
                Email mailClient = new Email();
                StringBuilder bodyClient = new StringBuilder();
                mail.mailSubject = "Quote request";
                mail.toAddresses = request.email;
                bodyClient.Append("Dear: " + request.FirstName + " " + request.LastName + "\n\n");
                bodyClient.Append("You requested a quote to BTFB your new request is in the hands of our specialists and they will be contacting you soon.\n");
                // bodyClient.Append("Your Request Id is " + request.RequestId+"\n\n");
                bodyClient.Append("Thank you for choosing BTFB \n");


                mail.msgbody = bodyClient;
                mail.SendEmail();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return RedirectToAction("Index", "Home", new { area = "" });

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
            return PartialView("_ModelPartial", request);
        }
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = await db.Requests.FindAsync(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }
        [Authorize(Roles = "Admins")]
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.Requests.ToListAsync());
        //}
        public async Task<ActionResult> Index(string filter = "")
        {
            List<Request> req = new List<Request>(); 
            if (filter != string.Empty && filter != null)
            {
                req = await db.Requests.Where(x => x.RequestId.Contains(filter) || x.email.Contains(filter)).ToListAsync();
            }
            else
            {
                req = await db.Requests.ToListAsync();
            }
            return View(req);
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = await db.Requests.FindAsync(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            RequestViewModel requestvm = new RequestViewModel();
            requestvm.Request = request;

            MakesDataAccess makesDA = new MakesDataAccess();
            requestvm.Makes = makesDA.GetMakesList();
            requestvm.Models = makesDA.GetModelsList(request.Make.GetValueOrDefault());
            if (request.Make != null)
            {
                requestvm.SelectedMake = request.Make.GetValueOrDefault().ToString();
            }
            if (request.Model!= null)
            {
                requestvm.SelectedModel = request.Model.GetValueOrDefault().ToString();
            }           
            UsersDataAccess usersDA = new UsersDataAccess();
            requestvm.Users = usersDA.GetNonAdminUsers();
            requestvm.SelectedUser = request.UserId;

            StatesDataAccess statesDA = new StatesDataAccess();
            requestvm.States = statesDA.GetStatesList();

            if (request.FromState!=null)
            {
                requestvm.FromState = request.FromState.GetValueOrDefault().ToString();
            }
            if (request.ToState != null)
            {
                requestvm.ToState = request.ToState.GetValueOrDefault().ToString();
            }
            Utils util = new Utils();
            requestvm.Years = util.GetYearsList();
            requestvm.SelectedYear = request.Year;

            return View(requestvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Edit(RequestViewModel rvm)
        {
            if (ModelState.IsValid)
            {

                Request request = new Request();
                request = rvm.Request;

                request.Make = int.Parse(rvm.SelectedMake);
                request.Model = int.Parse(rvm.SelectedModel);
                request.FromState = int.Parse(rvm.FromState);
                request.ToState = int.Parse(rvm.ToState);
                request.Year = rvm.SelectedYear;
                request.UserId = rvm.SelectedUser;
                if (request.UserId == "0")
                {
                    request.UserId = null;
                }

                db.Entry(request).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            MakesDataAccess makesDA = new MakesDataAccess();
            rvm.Makes = makesDA.GetMakesList();
            rvm.Models = makesDA.GetModelsList();

            UsersDataAccess usersDA = new UsersDataAccess();
            rvm.Users = usersDA.GetNonAdminUsers();           

            StatesDataAccess statesDA = new StatesDataAccess();
            rvm.States = statesDA.GetStatesList();


            Utils util = new Utils();
            rvm.Years = util.GetYearsList();
            return View(rvm);
        }
        //for next version use httpost to delete using a popup in the same index view with viewbag having form to submit
        //id to delete
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(int id)
        {
            Request request = await db.Requests.FindAsync(id);
            db.Requests.Remove(request);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admins")]
        public ActionResult Create()
        {
            RequestViewModel request = new RequestViewModel();
            request.Request = new Request();

            MakesDataAccess makesDA = new MakesDataAccess();
            request.Makes = makesDA.GetMakesList();
            request.Models = makesDA.GetModelsList();


            StatesDataAccess statesDA = new StatesDataAccess();
            request.States = statesDA.GetStatesList();

            UsersDataAccess usersDA = new UsersDataAccess();
            request.Users = usersDA.GetNonAdminUsers();

            Utils util = new Utils();
            request.Years = util.GetYearsList();
            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Create(RequestViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                Request request = new Request();
                request = rvm.Request;
                if(rvm.SelectedUser!="0")
                {
                    request.UserId = rvm.SelectedUser;
                }
                request.Make = int.Parse(rvm.SelectedMake);
                request.Model = int.Parse(rvm.SelectedModel);
                request.FromState = int.Parse(rvm.FromState);
                request.ToState = int.Parse(rvm.ToState);
                request.Year = rvm.SelectedYear;
                request.Make = int.Parse(rvm.SelectedMake);
                request.Model = int.Parse(rvm.SelectedModel);


                db.Requests.Add(request);
                await db.SaveChangesAsync();
               

                return RedirectToAction("Index", "Request");
            }
            return RedirectToAction("Index", "Request");
        }


    }
}