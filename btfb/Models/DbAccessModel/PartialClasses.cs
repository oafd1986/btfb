using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace btfb.Models.DbAccessModel
{
    [MetadataType(typeof(RequestsMetadata))]
    public partial class Request
    {
        public string MakeName
        {
            get
            {
                var makename = string.Empty;
                if (this.Make1 != null && this.Make1.Make1 != null)
                    makename = this.Make1.Make1;

                return makename;
            }
        }
        public string ModelName
        {
            get
            {
                var modelname = string.Empty;
                if(this.Model1!=null && this.Model1.Model1 != null)
                {
                    modelname = this.Model1.Model1;
                }
                return modelname;
            }
        }
    }
}