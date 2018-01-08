using System;
using Octokit;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using notgitter.Models;

namespace notgitter.Controllers {
    public partial class HomeController : Controller {

        // GET: ChatRoom
        [HttpGet]
        public ActionResult Chatroom() {

            List<Message> messages = new List<Message>();
            messages = getMessage();


            return View(messages);
        }

        //Adds message into Message database
        [HttpPost]
        public ActionResult Chatroom(string inputMessage) {
            NotGitterDBEntities db = new NotGitterDBEntities();
            List<Message> messages = new List<Message>();
            string repoName = "";

            //inputMessage is empty
            if (inputMessage.Equals("")) {
                messages = getMessage();
                return View(messages);
            }

            // Get CurrentTime
            DateTime currentTime = DateTime.Now;

            //Get Repo Name from url parameter
            if (Request.QueryString["repoName"] != null) {
                repoName = Request.QueryString["repoName"];
            }

            //Get current user
            int userId = Convert.ToInt32(Session["userId"]);

            //Get All Repo made by userId
            ICollection<Repo> allRepo = db.Repoes.Where(rp => rp.UId == userId).ToList();
            Repo repo = new Repo();

            // Loop through All repo made by userid, find repo name that are corresponding repo chatroom name 
            foreach (Repo rp in allRepo) {
                if (rp.name.Equals(repoName)) {
                    repo = rp;
                    continue;
                }
            }

            //Create Message object for new message
            Message newMessage = new Message();

            //Add information of new Message
            newMessage.Content = inputMessage;
            newMessage.Uid = userId;
            newMessage.timestamp = currentTime;
            newMessage.RepoId = repo.RepoId;

            //Add to Database
            db.Messages.Add(newMessage);

            //save changes
            db.SaveChanges();

            //Getting all message for listing
            messages = getMessage();

            //Return to chatroom view with list of messages
            return View(messages);
        }

        //Helper function to return list of messages to display
        public List<Message> getMessage() {
            NotGitterDBEntities db = new NotGitterDBEntities();
            string repoName = "";

            //Get Repo Name from url parameter
            if (Request.QueryString["repoName"] != null) {
                repoName = Request.QueryString["repoName"];
            }

            Repo repo = new Repo();
            ICollection<Repo> repoes = db.Repoes.Where(rp => rp.name == repoName).ToList();
            List<long> repoid = new List<long>();
            List<Message> messages = new List<Message>();

            // Get All the Repo ID that have same Repo name
            foreach (Repo rp in repoes) {
                repoid.Add(rp.RepoId);
            }

            // Get all the Message that contain repoid
            for (int i = 0; i < repoid.Count(); i++) {
                long actualRepoId = repoid.ElementAt(i);
                ICollection<Message> oneRepoMessages = db.Messages.Where(m => m.RepoId == actualRepoId).ToList();
                foreach (Message m in oneRepoMessages) {
                    messages.Add(m);
                }
            }

            //swap message according to timestamp
            var message = from m in messages orderby m.timestamp select m;


            return message.ToList();
        }
    }
}