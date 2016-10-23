using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteSharingApp.Models
{
    

    public class User
    {
       
        public User()
        {
            this.Documents = new HashSet<Document>();
            this.UserComments = new HashSet<UserComment>();
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Occupation { get; set; }
        public string College_org { get; set; }
        public string Enrollement_year { get; set; }
        public string Semester { get; set; }
        public string Program_field { get; set; }
        public string EmailId { get; set; }

       
        public virtual ICollection<Document> Documents { get; set; }
       
        public virtual ICollection<UserComment> UserComments { get; set; }
    }
}
