using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using notgitter;

namespace notgitter.Controllers
{
    public class RepoesController : Controller
    {
        private ChatroomModelContainer db = new ChatroomModelContainer();

        // GET: Repoes
        public async Task<ActionResult> Index()
        {
            var repoes = db.Repoes.Include(r => r.owner);
            return View(await repoes.ToListAsync());
        }

        // GET: Repoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repo repo = await db.Repoes.FindAsync(id);
            if (repo == null)
            {
                return HttpNotFound();
            }
            return View(repo);
        }

        // GET: Repoes/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.Users1, "Id", "name");
            return View();
        }

        // POST: Repoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,userId,dateCreated,language,name,url,private")] Repo repo)
        {
            if (ModelState.IsValid)
            {
                db.Repoes.Add(repo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.Users1, "Id", "name", repo.userId);
            return View(repo);
        }

        // GET: Repoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repo repo = await db.Repoes.FindAsync(id);
            if (repo == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.Users1, "Id", "name", repo.userId);
            return View(repo);
        }

        // POST: Repoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,userId,dateCreated,language,name,url,private")] Repo repo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.Users1, "Id", "name", repo.userId);
            return View(repo);
        }

        // GET: Repoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repo repo = await db.Repoes.FindAsync(id);
            if (repo == null)
            {
                return HttpNotFound();
            }
            return View(repo);
        }

        // POST: Repoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Repo repo = await db.Repoes.FindAsync(id);
            db.Repoes.Remove(repo);
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
    }
}
