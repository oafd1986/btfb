using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using btfb.Models.DbAccessModel;
using btfb.Models.DataAccessClasses;

namespace btfb.Controllers
{
    public class RequestsController : Controller
    {
        private btfbEntities db = new btfbEntities();

        // GET: Requests
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Index()
        {
            var requests = db.Requests.Include(r => r.AspNetUser).Include(r => r.Make1).Include(r => r.Model1).Include(r => r.State).Include(r => r.State1);
            return View(await requests.ToListAsync());
        }
       
        // GET: Requests/Details/5
        [Authorize(Roles = "admin")]
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

        // GET: Requests/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Make = new SelectList(db.Makes, "Id", "Make1");
            ViewBag.Model = new SelectList(db.Models, "id", "Model1");
            ViewBag.FromState = new SelectList(db.States, "Id", "State1");
            ViewBag.ToState = new SelectList(db.States, "Id", "State1");
            
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([Bind(Include = "FirstName,UserId,Fromaddress,FromZipCode,FromCity,FromState,ToAddress,ToZipCode,ToCity,ToState,Runs,Price,Make,Model,Year")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", request.UserId);
            ViewBag.Make = new SelectList(db.Makes, "Id", "Make1", request.Make);
            ViewBag.Model = new SelectList(db.Models, "id", "Model1", request.Model);
            ViewBag.FromState = new SelectList(db.States, "Id", "State1", request.FromState);
            ViewBag.ToState = new SelectList(db.States, "Id", "State1", request.ToState);
            return View(request);
        }

        // GET: Requests/Edit/5
        [Authorize(Roles = "admin")]
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
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", request.UserId);
            ViewBag.Make = new SelectList(db.Makes, "Id", "Make1", request.Make);
            ViewBag.Model = new SelectList(db.Models, "id", "Model1", request.Model);
            ViewBag.FromState = new SelectList(db.States, "Id", "State1", request.FromState);
            ViewBag.ToState = new SelectList(db.States, "Id", "State1", request.ToState);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit([Bind(Include = "RecordId,UserId,Fromaddress,FromZipCode,FromCity,FromState,ToAddress,ToZipCode,ToCity,ToState,Runs,Price,Make,Model,Year")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", request.UserId);
            ViewBag.Make = new SelectList(db.Makes, "Id", "Make1", request.Make);
            ViewBag.Model = new SelectList(db.Models, "id", "Model1", request.Model);
            ViewBag.FromState = new SelectList(db.States, "Id", "State1", request.FromState);
            ViewBag.ToState = new SelectList(db.States, "Id", "State1", request.ToState);
            return View(request);
        }

        // GET: Requests/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Request request = await db.Requests.FindAsync(id);
            db.Requests.Remove(request);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Models(int? makeId)
        {
            MakesDataAccess makesDA = new MakesDataAccess();
            List<Model> models = new List<Model>();
            models.Add(new Model { id = 0, Model1 = "-Model-" });
            if (makeId != null)
            {
                models.AddRange(makesDA.GetModelsFromMake(makeId.GetValueOrDefault()));
            }
            ViewBag.Model = new SelectList(models, "id", "Model1"); 
            return PartialView("_ModelPartial");
        }
        [AllowAnonymous]
        public ActionResult Models()
        {
            List<Model> models = new List<Model>();
            models.Add(new Model { id = 0, Model1 = "-Model-" });
            ViewBag.Model = new SelectList(models, "id", "Model1");
            return PartialView("_ModelPartial");
        }
        [AllowAnonymous]
        public ActionResult HomeCreate()
        {
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
            return View();
        }
    }
}
