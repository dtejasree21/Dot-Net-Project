//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobPostLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobPost
    {
        public int PostId { get; set; }
        public string JobId { get; set; }
        public Nullable<System.DateTime> DOP { get; set; }
        public Nullable<System.DateTime> LastDate { get; set; }
        public int Vacancies { get; set; }
    
        public virtual Job Job { get; set; }
    }
}
