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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace btfb.Controllers
{
    [Authorize]
    public class MakesController : Controller
    {
        private btfbEntities db = new btfbEntities();

        // GET: Makes
        public async Task<ActionResult> Index()
        {
            return View(await db.Makes.ToListAsync());
        }

        // GET: Makes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Make make = await db.Makes.FindAsync(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            return View(make);
        }

        // GET: Makes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Makes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Make1")] Make make)
        {
            if (ModelState.IsValid)
            {
                db.Makes.Add(make);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(make);
        }

        // GET: Makes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Make make = await db.Makes.FindAsync(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            return View(make);
        }

        // POST: Makes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Make1")] Make make)
        {
            if (ModelState.IsValid)
            {
                db.Entry(make).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(make);
        }

        // GET: Makes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Make make = await db.Makes.FindAsync(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            return View(make);
        }

        // POST: Makes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Make make = await db.Makes.FindAsync(id);
            db.Makes.Remove(make);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet,ActionName("Import")]
        public void ImportMakes()
        {
            string url = "https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var tmp = client.GetAsync(url).Result;
                if (tmp.IsSuccessStatusCode)
                {
                    var result = tmp.Content.ReadAsStringAsync();
                  
                }
                
            }
            catch (Exception)
            {
                // error handling
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
