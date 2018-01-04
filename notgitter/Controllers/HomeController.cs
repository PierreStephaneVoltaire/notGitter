using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using notgitter.Models;

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
            try
            {
                // The following requests retrieves all of the user's repositories and
                // requires that the user be logged in to work.
                var repositories = await client.Repository.GetAllForCurrent();

                //add repos and user to DB
               
                var userDetails = await client.User.Current();

                Models.User user = new Models.User();


                NotGitterDBEntities db = new NotGitterDBEntities();
                user.email= client.User.Email.GetAll().Result.ToArray()[0].Email;
                user.name = userDetails.Login;
                user.online = 1;
                user.GithubId = client.User.Current().Result.Id;
                //write your id here
                Session["userid"] = user;
                Models.User checkuser = db.Users.Where(d => d.GithubId == user.GithubId).Single();
                if (checkuser != null)
                {
                    db.Users.Add(user);
                }
                else {

                    

                }





                //d.Result.ToArray()[0].Email;
                //then add the repos if they don't exist
                //note: at this point you'd have to create a chat room for each repo that doesn't exist
                //index should show a list of all the repos(chat rooms available)
                // don't forget to get the id of the owner
                // TODO: make a viewModel for this mess
                // TODO: the view for this page should NOT show the  chat
                // index(list of repos)->(repos' chat panel)->(repo's details)
                // ViewBag.user = user;

                return View();
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