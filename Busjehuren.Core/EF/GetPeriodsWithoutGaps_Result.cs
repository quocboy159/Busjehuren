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
    
    public partial class GetPeriodsWithoutGaps_Result
    {
        public Nullable<int> Id { get; set; }
        public int CamperId { get; set; }
        public System.DateTime PeriodeVan { get; set; }
        public System.DateTime PeriodeTot { get; set; }
        public Nullable<decimal> BasisPrijs { get; set; }
        public Nullable<decimal> BasisPrijs35 { get; set; }
        public Nullable<decimal> BasisPrijsWeek { get; set; }
        public Nullable<decimal> BasisPrijsWeekend { get; set; }
        public Nullable<decimal> BasisPrijs829 { get; set; }
        public Nullable<decimal> BasisPrijs30 { get; set; }
        public Nullable<decimal> InventarisPrijsPerPersoon { get; set; }
        public Nullable<decimal> InventarisPrijs { get; set; }
        public bool IsActief { get; set; }
        public Nullable<decimal> VluchtPrijsEuro { get; set; }
        public Nullable<decimal> VluchtTaxEuro { get; set; }
        public Nullable<decimal> KortingPercOpCamper { get; set; }
        public Nullable<decimal> KortingPercOpTotaal { get; set; }
        public Nullable<decimal> KortingOpVluchtEuro { get; set; }
        public Nullable<decimal> VastePrijsEuro { get; set; }
        public bool IsBestGeboekt { get; set; }
        public Nullable<decimal> BestGeboektPrijsEuro { get; set; }
        public Nullable<int> BestGeboektDuur { get; set; }
        public Nullable<decimal> VanafPrijs { get; set; }
        public int PrijsLijstId { get; set; }
        public int RowOrder { get; set; }
    }
}
