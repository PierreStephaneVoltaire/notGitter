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
        GitAPI git=new GitAPI();
        NotGitterDBEntities db = new NotGitterDBEntities();

        // GET: ChatRoom
        public ActionResult Index()
        {
            List<Message> messages = new List<Message>();
            messages = getMessage();

            return View(messages);
        }
        
        [HttpGet]
        public ActionResult MessageAdd(string inputMessage) {
            
            // Get CurrentTime
            DateTime currentTime = DateTime.Now;

            //Get current user
            int userId = Convert.ToInt32(TempData["userId"]);
            Models.User user = new Models.User();
            user = db.Users.Where(oneUser => oneUser.UId ==  userId).First();

            //Get Current Repo
            Repo repo = new Repo();
            repo = db.Repoes.Where(rp => rp.UId == userId).FirstOrDefault<Repo>();

            Message newMessage = new Message();

            newMessage.Content = inputMessage;
            newMessage.UserName = user.name;
            newMessage.Uid = user.UId;
            newMessage.timestamp = currentTime;
            newMessage.RepoId = repo.RepoId;

            //Add to Database
            db.Messages.Add(newMessage);

            //save changes
            db.SaveChanges();

            //Getting all message for listing
            List<Message> messages = new List<Message>();
            messages = getMessage();

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
            else
            {   //if reponame is not exist send back to repositary view
                
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
                ICollection<Message> oneRepoMessages = db.Messages.Where(m => m.RepoId == repoid[i]).ToList();
                foreach (Message m in oneRepoMessages)
                {
                    messages.Add(m);
                }
            }
            //Need to swap message according to timestamp

            return messages;
        }
    }
} 