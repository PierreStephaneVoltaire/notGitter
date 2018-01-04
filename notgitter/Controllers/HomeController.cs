using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using notgitter.Models;
using System.Data.Entity;
namespace notgitter.Controllers
{   


    public class HomeController : Controller
    {
        // TODO: Replace the following values with the values from your application registration. Register an
        // application at https://github.com/settings/applications/new to get these values.
        const string clientId = "ebc5b3174e7840ea3164";
        private const string clientSecret = "36befd087000855e599f32cec03c1724da012164";
        readonly GitHubClient client =
            new GitHubClient(new ProductHeaderValue("FinalProject"), new Uri("https://github.com/"));

        // This URL uses the GitHub API to get a list of the current user's
        // repositories which include public and private repositories.

     

        public async Task<ActionResult> Index()
        {
            var accessToken = Session["OAuthToken"] as string;
            if (accessToken != null)
            {
                // This allows the client to make requests to the GitHub API on the user's behalf
                // without ever having the user's OAuth credentials.
                client.Credentials = new Credentials(accessToken);
            }
            else {

                return Redirect(GetOauthLoginUrl());
            }
            try
            {
                // The following requests retrieves all of the user's repositories and
                // requires that the user be logged in to work.
             
                //add repos and user to DB
               
                var userDetails = await client.User.Current();

                Models.User user = new Models.User();


                NotGitterDBEntities db = new NotGitterDBEntities();
                user.email= client.User.Email.GetAll().Result.ToArray()[0].Email;
                user.name = userDetails.Login;
                user.online = 1;
                var id=0;
                user.GithubId = client.User.Current().Result.Id;
                //write your id here
              
                Models.User checkuser = db.Users.Where(d => d.GithubId == user.GithubId).FirstOrDefault();
                
                if (checkuser == null)
                {
                    db.Users.Add(user);
                    id = user.UId;
                }
                else {
                    db.Entry(checkuser).State = EntityState.Modified;
                    id = checkuser.UId; 
                }
                IReadOnlyList<Repository> repos = await client.Repository.GetAllForCurrent();
                System.Diagnostics.Debug.Write(repos.Count);
                
                foreach (Repository e in repos)
                {
                    Repo newone = new Repo();
                    newone.UId = id;
                    newone.dateCreated = e.CreatedAt.DateTime;
                    newone.language = e.Language;
                    newone.C_private_ = e.Private ? 1 : 0;
                    newone.RepoId = e.Id;
                    newone.name = e.Name;
                    db.Repoes.Add(newone);
                   

                    Repo repo = db.Repoes.Find(e.Id);
                    if (repo != null)
                    { db.Entry(newone).State = EntityState.Modified; }

                    else
                    {
                        newone.UId = id;
                        newone.dateCreated = e.CreatedAt.DateTime;
                        newone.language = e.Language;
                        newone.C_private_ = e.Private ? 1 : 0;
                        newone.RepoId = e.Id;
                        newone.name = e.Name;
                        db.Repoes.Add(newone);
                    }
                

                }


                db.SaveChanges();

                //d.Result.ToArray()[0].Email;
                //then add the repos if they don't exist
                //note: at this point you'd have to create a chat room for each repo that doesn't exist
                //index should show a list of all the repos(chat rooms available)
                // don't forget to get the id of the owner
                // TODO: make a viewModel for this mess
                // TODO: the view for this page should NOT show the  chat
                // index(list of repos)->(repos' chat panel)->(repo's details)
                // ViewBag.user = user;
                
                return View(db.Repoes.Where(e=>e.UId==id).ToList());
            }
            catch (AuthorizationException)
            {
                // Either the accessToken is null or it's invalid. This redirects
                // to the GitHub OAuth login page. That page will redirect back to the
                // Authorize action.
                return Redirect(GetOauthLoginUrl());
            }
        }
        public async Task<ActionResult> Authorize(string code, string state)
        {
            if (!String.IsNullOrEmpty(code))
            {
                var expectedState = Session["CSRF:State"] as string;
                if (state != expectedState) throw new InvalidOperationException("SECURITY FAIL!");
                Session["CSRF:State"] = null;

                var token = await client.Oauth.CreateAccessToken(
                    new OauthTokenRequest(clientId, clientSecret, code)
                    {
                        RedirectUri = new Uri("http://localhost:59637/home/authorize")
                    });
                Session["OAuthToken"] = token.AccessToken;
            }

            return RedirectToAction("Index");
        }

        private string GetOauthLoginUrl()
        {
            string csrf = Membership.GeneratePassword(24, 1);
            Session["CSRF:State"] = csrf;

            // 1. Redirect users to request GitHub access
            var request = new OauthLoginRequest(clientId)
            {
                Scopes = { "user", "notifications" },
                State = csrf
            };
            var oauthLoginUrl = client.Oauth.GetGitHubLoginUrl(request);
            return oauthLoginUrl.ToString();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}