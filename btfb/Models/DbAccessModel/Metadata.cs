using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace btfb.Models.DbAccessModel
{
    public class RequestsMetadata
    {
        [Required]
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [Display(Name ="Phone")]
        public string phone { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Runs")]
        public bool Runs { get; set; }
        [Required]
        [Display(Name = "Origin Zip Code")]
        public string FromZipCode { get; set; }
        [Required]
        [Display(Name = "Destination Zip Code")]
        public string ToZipCode { get; set; }
    }
}