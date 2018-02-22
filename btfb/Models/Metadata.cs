using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace btfb.Models
{
    public class RequestsMetadata
    {
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please Enter Correct Email Address")]
        [Required]
        [Display(Name = "Email")]
        public string email;
        [Required]
        [Display(Name ="Phone")]
        public string phone;
    }
}