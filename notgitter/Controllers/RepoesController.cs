using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using notgitter.Models;

namespace notgitter.Controllers
{
    public class RepoesController : Controller
    {
        private NotGitterDBEntities db = new NotGitterDBEntities();

        // GET: Repoes
        public ActionResult Index()
        {
            var repoes = db.Repoes.Include(r => r.User);
            return View(repoes.ToList());
        }

        // GET: Repoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repo repo = db.Repoes.Find(id);
            if (repo == null)
            {
                return HttpNotFound();
            }
            return View(repo);
        }

        // GET: Repoes/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "name");
            return View();
        }

        // POST: Repoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,dateCreated,language,name,url,C_private_,UserId")] Repo repo)
        {
            if (ModelState.IsValid)
            {
                db.Repoes.Add(repo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "name", repo.UserId);
            return View(repo);
        }

        // GET: Repoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repo repo = db.Repoes.Find(id);
            if (repo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "name", repo.UserId);
            return View(repo);
        }

        // POST: Repoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,dateCreated,language,name,url,C_private_,UserId")] Repo repo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "name", repo.UserId);
            return View(repo);
        }

        // GET: Repoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repo repo = db.Repoes.Find(id);
            if (repo == null)
            {
                return HttpNotFound();
            }
            return View(repo);
        }

        // POST: Repoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repo repo = db.Repoes.Find(id);
            db.Repoes.Remove(repo);
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
    }
}
