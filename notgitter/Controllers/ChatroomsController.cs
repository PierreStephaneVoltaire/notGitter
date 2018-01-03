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
    public class ChatroomsController : Controller
    {
        private ChatroomModelContainer db = new ChatroomModelContainer();

        // GET: Chatrooms
        public async Task<ActionResult> Index()
        {
            return View(await db.Chatrooms.ToListAsync());
        }

        // GET: Chatrooms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatroom chatroom = await db.Chatrooms.FindAsync(id);
            if (chatroom == null)
            {
                return HttpNotFound();
            }
            return View(chatroom);
        }

        // GET: Chatrooms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chatrooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id")] Chatroom chatroom)
        {
            if (ModelState.IsValid)
            {
                db.Chatrooms.Add(chatroom);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(chatroom);
        }

        // GET: Chatrooms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatroom chatroom = await db.Chatrooms.FindAsync(id);
            if (chatroom == null)
            {
                return HttpNotFound();
            }
            return View(chatroom);
        }

        // POST: Chatrooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id")] Chatroom chatroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatroom).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chatroom);
        }

        // GET: Chatrooms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatroom chatroom = await db.Chatrooms.FindAsync(id);
            if (chatroom == null)
            {
                return HttpNotFound();
            }
            return View(chatroom);
        }

        // POST: Chatrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Chatroom chatroom = await db.Chatrooms.FindAsync(id);
            db.Chatrooms.Remove(chatroom);
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
