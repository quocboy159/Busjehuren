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
    
    public partial class ReservationMailArchive
    {
        public int Id { get; set; }
        public string MailTo { get; set; }
        public System.DateTime SendDate { get; set; }
        public int ReservationNumber { get; set; }
        public string MailBody { get; set; }
    }
}