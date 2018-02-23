using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using btfb.Models;
using System.ComponentModel.DataAnnotations;
namespace btfb.Models
{
    public partial class Request
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}