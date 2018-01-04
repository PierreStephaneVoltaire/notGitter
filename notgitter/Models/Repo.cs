//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace notgitter.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Repo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Repo()
        {
            this.Collaborators = new HashSet<User>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public string language { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public byte[] C_private_ { get; set; }
        public int UserId { get; set; }
    
        public virtual User Owner { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Collaborators { get; set; }
        public virtual Chatroom Chatroom { get; set; }
    }
}
