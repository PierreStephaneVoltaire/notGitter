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
    {  GitAPI git=new GitAPI();
       // GET: ChatRoom
        public ActionResult Index()
        {    
             return View();
        }
        
        public ActionResult MessageAdd(string message) {


            return View();


        }




    }
}