using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using btfb.Models.DbAccessModel;
using btfb.Models;
using btfb.Models.DataAccessClasses;
namespace btfb.ViewModels
{
    public class RequestViewModel
    {
        public Request Request { get; set; }
        public string SelectedMake{get;set;}
        public IEnumerable<SelectListItem> Makes { get; set; }
        public string SelectedModel { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }
        public string SelectedYear { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
        public string FromState { get; set; }        
        public string ToState { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
    
}