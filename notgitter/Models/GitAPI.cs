using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Octokit;

namespace notgitter.Models
{
    public class GitAPI
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public GitHubClient client { get; set; }
        public GitAPI() {
            clientId = "ebc5b3174e7840ea3164";
            clientSecret = "36befd087000855e599f32cec03c1724da012164";
            client= new GitHubClient(new ProductHeaderValue("FinalProject"), new Uri("https://github.com/"));
            
            
            }
       
    }
}