using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace notgitter.Models
{
    public class Users
    {
        public Users()
        {
            this.Messages = new HashSet<Message>();
            this.Repoes = new HashSet<Repo>();
    
        }

        public int Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int GithubId { get; set; }
        public bool online { get; set; }

       
        public virtual ICollection<Message> Messages { get; set; }
     
        public virtual ICollection<Repo> Repoes { get; set; }
    
    }
}
