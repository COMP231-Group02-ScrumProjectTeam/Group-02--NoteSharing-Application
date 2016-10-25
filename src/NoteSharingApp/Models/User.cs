using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string College { get; set; }
        public string Enrollement_year { get; set; }
        public string Semester { get; set; }
        public string Program_field { get; set; }
        [Display(Name = "Email address")]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Error message.")]
        public string User_name { get; set; }
        public string Password { get; set; }


        public virtual ICollection<Document> Documents { get; set; }
       
        public virtual ICollection<UserComment> UserComments { get; set; }
    }
}
