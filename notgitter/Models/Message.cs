using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace notgitter.Models
{
    public class Message
    {

        public int Id { get; set; }
        public string content { get; set; }
        public System.DateTime timestamp { get; set; }
        public int userId { get; set; }
        public int ChatroomId { get; set; }
       
        public virtual Users Users { get; set; }
    }
}