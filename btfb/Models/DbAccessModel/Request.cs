//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace btfb.Models.DbAccessModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public string RecordId { get; set; }
        public string UserId { get; set; }
        public string Fromaddress { get; set; }
        public string FromZipCode { get; set; }
        public string FromCity { get; set; }
        public Nullable<int> FromState { get; set; }
        public string ToAddress { get; set; }
        public string ToZipCode { get; set; }
        public string ToCity { get; set; }
        public Nullable<int> ToState { get; set; }
        public Nullable<byte> Runs { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
