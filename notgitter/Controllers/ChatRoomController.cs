using System;
using Octokit;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using notgitter.Models;
using System.Threading.Tasks;
namespace notgitter.Controllers
{
    public class ChatRoomController : Controller
    {  GitAPI git=new GitAPI();
       // GET: ChatRoom
        public ActionResult Index()
        {    
             return View();
        }
        [HttpGet]
        public ActionResult Index(string repoName,int userID)
        {
            return View();
        }
       



    }
}