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
    
    public partial class Longhire
    {
        public int Id { get; set; }
        public int Days { get; set; }
        public decimal Percentage { get; set; }
        public int LeverancierId { get; set; }
    
        public virtual Leverancier Leverancier { get; set; }
    }
}
