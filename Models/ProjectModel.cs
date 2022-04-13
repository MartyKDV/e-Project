using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_Project.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Title Name...")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter a Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Enter Author...")]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please Enter the Programming Language...")]
        [Display(Name = "Programming Language")]
        public string ProgrammingLanguage { get; set; }

        [Required(ErrorMessage = "Please Enter Status...")]
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
