//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ICYL.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class LookupValue
    {
        public int ValueId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public string Value { get; set; }
        public string ValueDescription { get; set; }
        public Nullable<bool> Active { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string APIId { get; set; }
        public string APIKey { get; set; }
    }
}
