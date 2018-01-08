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
    public partial class HomeController : Controller
    {
        // TODO: Replace the following values with the values from your application registration. Register an
        // application at https://github.com/settings/applications/new to get these values.


        // This URL uses the GitHub API to get a list of the current user's
        // repositories which include public and private repositories.
        GitAPI client = new GitAPI();
     

        public async Task<ActionResult> Index()
        {
            var accessToken = Session["OAuthToken"] as string;
            if (accessToken != null)
            {
                // This allows the client to make requests to the GitHub API on the user's behalf
                // without ever having the user's OAuth credentials.
                client.client.Credentials = new Credentials(accessToken);
               
            }
            else {

                return Redirect(GetOauthLoginUrl());
            }
            try
            {
                // The following requests retrieves all of the user's repositories and
                // requires that the user be logged in to work.
             
                //add repos and user to DB
               
                var userDetails = await client.client.User.Current();
               
                 Models.User currentUser = new Models.User();


                NotGitterDBEntities dbContextRef = new NotGitterDBEntities();
                var emailList = await client.client.User.Email.GetAll();
                currentUser.email= emailList.ToArray()[0].Email;
                currentUser.name = userDetails.Login;
                currentUser.online = 1;
                var currentUserId=0;
                var tempGithubId = await client.client.User.Current();
                currentUser.GithubId = tempGithubId.Id;

                //write your id here

                Models.User checkuser = dbContextRef.Users.Where(d => d.GithubId == currentUser.GithubId).FirstOrDefault();
                
                if (checkuser == null)
                {
                    dbContextRef.Users.Add(currentUser);
                    currentUserId = currentUser.UId;
                }
                else {
                    dbContextRef.Entry(checkuser).State = EntityState.Modified;

                    currentUserId = checkuser.UId; 
                }
                IReadOnlyList<Repository> repos = await client.client.Repository.GetAllForCurrent();

                 foreach (Repository e in repos)
                {
                    Models.Repo oldone = await dbContextRef.Repoes.Where(rp => rp.url == e.HtmlUrl).FirstOrDefaultAsync<Models.Repo>();
                    Models.Repo newone1 = new Models.Repo();
               
                    if (oldone != null)
                    {

                        oldone.UId = currentUserId;
                        oldone.url = e.HtmlUrl;
                        oldone.dateCreated = e.CreatedAt.DateTime;
                        oldone.language = e.Language;
                        oldone.C_private_ = e.Private ? 1 : 0;
                        oldone.RepoId = Convert.ToInt32(e.Id);
                        oldone.name = e.Name;
                        oldone.Stars = e.StargazersCount;
                        oldone.Description = e.Description;
                        dbContextRef.Entry(oldone).State = EntityState.Modified;


                    }
                    else
                    {
                        newone1.UId = currentUserId;
                        newone1.url = e.HtmlUrl;
                        newone1.dateCreated = e.CreatedAt.DateTime;
                        newone1.language = e.Language;
                        newone1.C_private_ = e.Private ? 1 : 0;
                        newone1.RepoId = Convert.ToInt32(e.Id);
                        newone1.name = e.Name;
                        newone1.Description = e.Description;
                        newone1.Stars = e.StargazersCount;
                        dbContextRef.Repoes.Add(newone1);
                      
                    }
                    try
                    {

                        dbContextRef.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }


                //d.Result.ToArray()[0].Email;
                //then add the repos if they don't exist
                //note: at this point you'd have to create a chat room for each repo that doesn't exist
                //index should show a list of all the repos(chat rooms available)
                // don't forget to get the id of the owner
                // TODO: make a viewModel for this mess
                // TODO: the view for this page should NOT show the  chat
                // index(list of repos)->(repos' chat panel)->(repo's details)
                ViewBag.name = currentUser.name;
                Session["userId"]= currentUserId;
                Session["userName"] = currentUser.name;

                return View(dbContextRef.Repoes.Where(e=>e.UId==currentUserId).ToList());
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

                var token = await client.client.Oauth.CreateAccessToken(
                    new OauthTokenRequest(client.clientId, client.clientSecret, code)
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
            var request = new OauthLoginRequest(client.clientId)
            {
                Scopes = { "user", "notifications" },
                State = csrf
            };
            var oauthLoginUrl = client.client.Oauth.GetGitHubLoginUrl(request);
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