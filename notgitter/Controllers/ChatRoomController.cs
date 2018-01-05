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

            var repoName = "";
            //Get Repo Name
            if (Request.QueryString["repoName"] != null)
            {
                repoName = Request.QueryString["repoName"];
            }
            else
            {
                return Redirect("Index");
            }
            


            return View();
        }
        
        [HttpGet]
        public ActionResult MessageAdd(string message) {

            var repoName = "";
            //Get Repo Name
            if (Request.QueryString["repoName"] != null)
            {
                repoName = Request.QueryString["repoName"];
            }
            else
            {
                return Redirect("Index");
            }

            DateTime currentTime = DateTime.Now;

            //Get current user
            Models.User user = new Models.User();
            user = db.Users.Where(a => a.GithubId == git.client.User.Current().Result.Id).FirstOrDefault();

            //Get Current Repo
            Repo repo = new Repo();
            repo = db.Repoes.Where(rp => rp.name == repoName).FirstOrDefault<Repo>();

            Message newMessage = new Message();
            var repoId = 0;
            //check repoid

           // newMessage.Content = this.newMessageInput.Text; //text content
            newMessage.UserName = user.name;
            newMessage.Uid = user.UId;
            newMessage.timestamp = currentTime;
            newMessage.RepoId = repo.RepoId;

            repo.Messages.Add(newMessage);

           // db.Repoes.

            db.SaveChanges();



            return View(db.Repoes.Where(rp=>rp.name == repoName).ToList());


        }



    }
}