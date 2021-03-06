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
    
    public partial class Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            this.People = new HashSet<Person>();
            this.ReservationPackages = new HashSet<ReservationPackage>();
        }
    
        public int Id { get; set; }
        public int Status { get; set; }
        public string ReservationNumber { get; set; }
        public System.DateTime ReservationDate { get; set; }
        public decimal GrossAmount { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public System.DateTime PickupDate { get; set; }
        public System.DateTime ReturnDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public string EditBy { get; set; }
        public int DestinationId { get; set; }
        public string SnapShot { get; set; }
        public Nullable<int> ReservationLanguageId { get; set; }
        public string SupplierConfirmationCode { get; set; }
        public Nullable<int> TotalAdults { get; set; }
        public Nullable<int> TotalChildren { get; set; }
        public Nullable<int> PartnerId { get; set; }
        public bool AfwijkendFactuuradres { get; set; }
        public string FactuurBedrijfsnaam { get; set; }
        public string FactuurEmail { get; set; }
        public string FactuurTelefoon { get; set; }
        public string FactuurStraat { get; set; }
        public string FactuurHuisnummer { get; set; }
        public string FactuurPostcode { get; set; }
        public string FactuurPlaats { get; set; }
        public string CompanyName { get; set; }
        public string DestinationCountry { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> People { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservationPackage> ReservationPackages { get; set; }
    }
}
