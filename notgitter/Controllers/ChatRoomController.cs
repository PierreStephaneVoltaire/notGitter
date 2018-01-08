using System;
using Octokit;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using notgitter.Models;

namespace notgitter.Controllers
{
    public class ChatRoomController : Controller
    {
        NotGitterDBEntities db = new NotGitterDBEntities();

        // GET: ChatRoom
        public ActionResult Index()
        {
            List<Message> messages = new List<Message>();
            messages = getMessage();

            return View(messages);
        }

        [HttpGet]
        public ActionResult MessageAdd(string inputMessage)
        {

            // Get CurrentTime
            DateTime currentTime = DateTime.Now;

            //Get Repo Name from url parameter
            if (Request.QueryString["repoName"] != null)
            {
                repoName = Request.QueryString["repoName"];
            }

            //Get current user
            int userId = Convert.ToInt32(TempData["userId"]);

            //Get All Repo made by userId
            ICollection<Repository> allRepo = db.Repoes.Where(rp => rp.UId == userId).ToList();
            Repo repo = new Repo();

            // Loop through All repo made by userid, find repo name that are corresponding repo chatroom name 
            foreach (Repo rp in allRepo)
            {
                if(rp.name == repoName)
                {
                    repo = rp;
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
            List<Message> messages = new List<Message>();
            messages = getMessage();

            //Return to chatroom view with list of messages
            return View(messages);
        }

        public List<Message> getMessage()
        {
            string repoName = "";

            //Get Repo Name from url parameter
            if (Request.QueryString["repoName"] != null)
            {
                repoName = Request.QueryString["repoName"];
            }

            Repo repo = new Repo();
            ICollection<Repo> repoes = db.Repoes.Where(rp => rp.name == repoName).ToList();
            List<long> repoid = new List<long>();
            List<Message> messages = new List<Message>();

            // Get All the Repo ID that have same Repo name
            foreach (Repo rp in repoes)
            {
                repoid.Add(rp.RepoId);
            }

            // Get all the Message that contain repoid
            for (int i = 0; i < repoid.Count(); i++)
            {
                long actualRepoId = repoid.ElementAt(i);
                ICollection<Message> oneRepoMessages = db.Messages.Where(m => m.RepoId == actualRepoId).ToList();
                foreach (Message m in oneRepoMessages)
                {
                    messages.Add(m);
                }
            }

            //swap message according to timestamp
            var message = from m in messages orderby m.timestamp select m;


            return message.ToList();
        }
    }
}