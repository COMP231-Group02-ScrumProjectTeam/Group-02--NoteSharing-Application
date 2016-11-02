using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoteSharingApp.Models
{
    public class UserForDisplay
    {
        public int ID { get; set; }
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
    }

   [ModelMetadataType(typeof(UserMD))]
    public partial class User
    {
        public partial class UserMD
        {


            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string College { get; set; }
            public string Enrol_year { get; set; }
            public string Semester { get; set; }
            public string Program { get; set; }
            [Required]
            [Display(Name = "User name")]
            public string User_name { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            //[Display(Name = "Remember on this computer")]
            //public bool RememberMe { get; set; }
           



        }

    }

   
}