//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tour.DomainModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class LocationDetail
    {
        public int LocationDetailsId { get; set; }
        public string LocationTypeID { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationAddress3 { get; set; }
        public string LocationTown { get; set; }
        public string LocationCountyValue { get; set; }
        public string LocationEircode { get; set; }
        public string LocationEmail { get; set; }
        public string LocationPhone { get; set; }
        public string LocationURL { get; set; }
        public byte[] LocationImage { get; set; }
        public Nullable<int> LocationLongitude { get; set; }
        public Nullable<int> LocationLatitude { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string EditedBy { get; set; }
        public Nullable<System.DateTime> EditedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
