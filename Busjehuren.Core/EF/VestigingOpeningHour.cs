//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Busjehuren.Core.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class VestigingOpeningHour
    {
        public int Id { get; set; }
        public int VestigingId { get; set; }
        public int Weekday { get; set; }
        public string WeekdayName { get; set; }
        public Nullable<decimal> FromHour { get; set; }
        public Nullable<decimal> ToHour { get; set; }
    
        public virtual Vestiging Vestiging { get; set; }
    }
}
