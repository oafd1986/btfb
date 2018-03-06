using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using btfb.Models.DbAccessModel;
using btfb.Models.DataAccessClasses;

namespace btfb.Models
{
    public class ComboBoxViewModels
    {
        
        public string ComboId { get; set; }
        public string SelectedOption { get; set; }
        public IEnumerable<SelectListItem> SelectOptions { get; set; }
    }
   
    
}