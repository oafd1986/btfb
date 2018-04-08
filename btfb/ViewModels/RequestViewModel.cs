using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using btfb.Models.DbAccessModel;
using btfb.Models;
using btfb.Models.DataAccessClasses;
using System.ComponentModel.DataAnnotations;
namespace btfb.ViewModels
{
    public class RequestViewModel
    {
        public Request Request { get; set; }
       
        [Range(1, int.MaxValue, ErrorMessage = "Select a Make")]
        public string SelectedMake{get;set;}
        public IEnumerable<SelectListItem> Makes { get; set; }

        [Display(Name = "Model")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Model")]
        public string SelectedModel { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }
        
        [Display(Name = "Year")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Year")]
        public string SelectedYear { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
       
        [Display(Name = "Origin State")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a State")]
        public string FromState { get; set; }
       
        [Display(Name = "Destination State")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a State")]
        public string ToState { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public string SelectedUser { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
    
}