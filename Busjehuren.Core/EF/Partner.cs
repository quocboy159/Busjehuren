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
    
    public partial class Partner
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public string IBAN { get; set; }
        public string BIC { get; set; }
        public string KVKNumber { get; set; }
        public string VATNumber { get; set; }
        public string Website { get; set; }
        public Nullable<int> NumberofVisitorAMonth { get; set; }
        public Nullable<int> Commission { get; set; }
        public Nullable<int> Status { get; set; }
        public string Postcode { get; set; }
        public string ActivateCode { get; set; }
    }
}
