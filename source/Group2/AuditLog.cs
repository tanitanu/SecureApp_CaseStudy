//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImageProcessing.Data.ImageUpload.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuditLog
    {
        public long Id { get; set; }
        public string UserID { get; set; }
        public Nullable<System.DateTime> UserLoginTime { get; set; }
        public Nullable<System.DateTime> UserLogoutTime { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    }
}
