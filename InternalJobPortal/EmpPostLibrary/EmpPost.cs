//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmpPostLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmpPost
    {
        public int PostId { get; set; }
        public string EmpId { get; set; }
        public System.DateTime ApplyDate { get; set; }
        public string ApplicationStatus { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual JobPost JobPost { get; set; }
    }
}
