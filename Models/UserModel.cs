using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Project.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Password...")]
        [StringLength(100, ErrorMessage = "Password length must be between {2} and {1} characters.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Email...")]
        [EmailAddress]
        [Remote(action: "VerifyEmail", controller: "Account")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter name...")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter phone number...")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
    public class LoginUser
    {
        [Required(ErrorMessage = "Please Enter Email...")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password...")]
        public string Password { get; set; }
    }
}
