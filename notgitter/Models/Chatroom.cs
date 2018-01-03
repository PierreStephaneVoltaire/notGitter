using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace notgitter.Models
{
    public class Chatroom
    {
        
        public Chatroom()
        {
            this.Messages = new HashSet<Message>();
            this.Repoes = new HashSet<Repo>();
        }

        public int Id { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Repo> Repoes { get; set; }
    }
}