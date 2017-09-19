using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConsuptionMaterials.Models;

namespace ConsuptionMaterials.Controllers
{
    public class ConsuptionsController : Controller
    {
        private ConsuptionContext db = new ConsuptionContext();

        // GET: Consuptions
        public ActionResult Index()
        {
            var consuptions = db.Consuptions.Include(c => c.Manager).Include(c => c.Material).Include(c => c.Person).OrderByDescending(c=> c.DateConsuption);
            return View(consuptions.ToList());
        }

        // GET: Consuptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consuption consuption = db.Consuptions.Find(id);
            if (consuption == null)
            {
                return HttpNotFound();
            }
            return View(consuption);
        }

        // GET: Consuptions/Create
        public ActionResult Create()
        {
            ViewBag.ManagerID = new SelectList(db.Managers, "Id", "Login");
            ViewBag.MaterialID = new SelectList(db.Materials.OrderBy(m => m.Name), "Id", "Name");
            ViewBag.PersonID = new SelectList(db.People.OrderBy(p => p.PersonName), "Id", "PersonName");
            return View();
        }

        protected int? GetManagerId()
        {
            int? result = null;

            var mans = (from m in db.Managers
                        where m.Login == System.Environment.UserName
                        select m).Single();
            try
            {
                result = mans.Id;
            }
            catch { }
            return result;
        }

        // POST: Consuptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,PersonID,MaterialID,DateConsuption,Count,Notes,ManagerID")] Consuption consuption)
        public ActionResult Create([Bind(Include = "Id,PersonID,MaterialID,Count,Notes")] Consuption consuption, int Operation)
        {
            if (ModelState.IsValid)
            {
                int cnt = 0;
                try
                {
                    cnt = Convert.ToInt32(consuption.Count);
                    if (Operation == 0)
                    {
                        cnt = (cnt > 0) ? cnt * -1 : cnt;
                    }
                    else
                    {
                        cnt = (cnt > 0) ? cnt : cnt * -1;
                    }
                }
                catch { }
                consuption.Count = cnt;
                consuption.ManagerID = GetManagerId();
                consuption.DateConsuption = DateTime.Now;
                db.Consuptions.Add(consuption);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ManagerID = new SelectList(db.Managers, "Id", "Login", consuption.ManagerID);
            ViewBag.MaterialID = new SelectList(db.Materials, "Id", "Name", consuption.MaterialID);
            ViewBag.PersonID = new SelectList(db.People.OrderBy(p => p.PersonName), "Id", "PersonName", consuption.PersonID);
            return View(consuption);
        }

        // GET: Consuptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consuption consuption = db.Consuptions.Find(id);
            if (consuption == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerID = new SelectList(db.Managers, "Id", "Login", consuption.ManagerID);
            ViewBag.MaterialID = new SelectList(db.Materials, "Id", "Name", consuption.MaterialID);
            ViewBag.PersonID = new SelectList(db.People.OrderBy(p => p.PersonName), "Id", "PersonName", consuption.PersonID);
            return View(consuption);
        }

        // POST: Consuptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonID,MaterialID,DateConsuption,Count,Notes,ManagerID")] Consuption consuption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consuption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerID = new SelectList(db.Managers, "Id", "Login", consuption.ManagerID);
            ViewBag.MaterialID = new SelectList(db.Materials, "Id", "Name", consuption.MaterialID);
            ViewBag.PersonID = new SelectList(db.People.OrderBy(p => p.PersonName), "Id", "PersonName", consuption.PersonID);
            return View(consuption);
        }

        // GET: Consuptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consuption consuption = db.Consuptions.Find(id);
            if (consuption == null)
            {
                return HttpNotFound();
            }
            return View(consuption);
        }

        // POST: Consuptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consuption consuption = db.Consuptions.Find(id);
            db.Consuptions.Remove(consuption);
            db.SaveChanges();
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

        public PartialViewResult LastConsuptions(int count = 20)
        {
            var last = db.Consuptions.Include(c => c.Material).Include(c => c.Person).OrderByDescending(c => c.DateConsuption).Take(count);

            return PartialView(last);
        }
    }
}
