//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TryOut_Dekstop1
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetailService
    {
        public string TransactionNumber { get; set; }
        public string ServiceCode { get; set; }
        public Nullable<int> Cost { get; set; }
    
        public virtual MotorcycleServices MotorcycleServices { get; set; }
        public virtual TransactionService TransactionService { get; set; }
    }
}