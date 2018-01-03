using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace notgitter.Models
{
    public class Repo
    {
        public Repo()
        {
            this.Collaborators= new HashSet<Users>();
        }

        public int Id { get; set; }
        public int userId { get; set; }
        public System.DateTime dateCreated { get; set; }
        public string language { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public bool @private { get; set; }
        public int Chatroom_Id { get; set; }

        public virtual Chatroom Chatroom { get; set; }
        public virtual Users Owner { get; set; }
     
        public virtual ICollection<Users> Collaborators { get; set; }
    }
}