using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteSharingApp.Models
{


    public partial class User
    {


        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string College { get; set; }
        public string Enrol_year { get; set; }
        public string Semester { get; set; }
        public string Program { get; set; }

        public string User_name { get; set; }
        public string Password { get; set; }
        //public bool RememberMe { get; set; }


        public virtual ICollection<Document> Documents { get; set; }

        public virtual ICollection<UserComment> UserComments { get; set; }
    }
}