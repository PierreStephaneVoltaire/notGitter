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
    public class ChatroomsController : Controller
    {
        private NotGitterDBEntities db = new NotGitterDBEntities();

        // GET: Chatrooms
        public ActionResult Index()
        {
            var chatrooms = db.Chatrooms.Include(c => c.Repo);
            return View(chatrooms.ToList());
        }

        // GET: Chatrooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatroom chatroom = db.Chatrooms.Find(id);
            if (chatroom == null)
            {
                return HttpNotFound();
            }
            return View(chatroom);
        }

        // GET: Chatrooms/Create
        public ActionResult Create()
        {
            ViewBag.ChatroomRepo_Chatroom_Id = new SelectList(db.Repoes, "Id", "language");
            return View();
        }

        // POST: Chatrooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ChatroomRepo_Chatroom_Id")] Chatroom chatroom)
        {
            if (ModelState.IsValid)
            {
                db.Chatrooms.Add(chatroom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChatroomRepo_Chatroom_Id = new SelectList(db.Repoes, "Id", "language", chatroom.ChatroomRepo_Chatroom_Id);
            return View(chatroom);
        }

        // GET: Chatrooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatroom chatroom = db.Chatrooms.Find(id);
            if (chatroom == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChatroomRepo_Chatroom_Id = new SelectList(db.Repoes, "Id", "language", chatroom.ChatroomRepo_Chatroom_Id);
            return View(chatroom);
        }

        // POST: Chatrooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ChatroomRepo_Chatroom_Id")] Chatroom chatroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChatroomRepo_Chatroom_Id = new SelectList(db.Repoes, "Id", "language", chatroom.ChatroomRepo_Chatroom_Id);
            return View(chatroom);
        }

        // GET: Chatrooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chatroom chatroom = db.Chatrooms.Find(id);
            if (chatroom == null)
            {
                return HttpNotFound();
            }
            return View(chatroom);
        }

        // POST: Chatrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chatroom chatroom = db.Chatrooms.Find(id);
            db.Chatrooms.Remove(chatroom);
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
