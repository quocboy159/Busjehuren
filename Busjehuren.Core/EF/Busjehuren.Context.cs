﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BusjehurenDbContext : DbContext
    {
        public BusjehurenDbContext()
            : base("name=busjehurenEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Adre> Adres { get; set; }
        public virtual DbSet<Bestand> Bestands { get; set; }
        public virtual DbSet<Camper> Campers { get; set; }
        public virtual DbSet<CamperAanbieding> CamperAanbiedings { get; set; }
        public virtual DbSet<CamperBed> CamperBeds { get; set; }
        public virtual DbSet<CamperBestanden> CamperBestandens { get; set; }
        public virtual DbSet<CamperEigenschappen> CamperEigenschappens { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<ContentType> ContentTypes { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<DestinationType> DestinationTypes { get; set; }
        public virtual DbSet<dtproperty> dtproperties { get; set; }
        public virtual DbSet<Eigenschap> Eigenschaps { get; set; }
        public virtual DbSet<EigenschapWaarde> EigenschapWaardes { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Leverancier> Leveranciers { get; set; }
        public virtual DbSet<Longhire> Longhires { get; set; }
        public virtual DbSet<Pakket> Pakkets { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PreImportCamperPrice> PreImportCamperPrices { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationMailArchive> ReservationMailArchives { get; set; }
        public virtual DbSet<ReservationPackage> ReservationPackages { get; set; }
        public virtual DbSet<SupplierContent> SupplierContents { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tbl_CamperOffers> tbl_CamperOffers { get; set; }
        public virtual DbSet<tbl_IpToCountry> tbl_IpToCountry { get; set; }
        public virtual DbSet<tbls_BlockedIPs> tbls_BlockedIPs { get; set; }
        public virtual DbSet<tbls_Categories> tbls_Categories { get; set; }
        public virtual DbSet<tbls_CategoriesXmlTypes> tbls_CategoriesXmlTypes { get; set; }
        public virtual DbSet<tbls_Categoryancestors> tbls_Categoryancestors { get; set; }
        public virtual DbSet<tbls_Categorylanguagevalues> tbls_Categorylanguagevalues { get; set; }
        public virtual DbSet<tbls_Categoryparents> tbls_Categoryparents { get; set; }
        public virtual DbSet<tbls_DomainSettings> tbls_DomainSettings { get; set; }
        public virtual DbSet<tbls_Dropdownlanguagevalues> tbls_Dropdownlanguagevalues { get; set; }
        public virtual DbSet<tbls_Dropdowns> tbls_Dropdowns { get; set; }
        public virtual DbSet<tbls_DropdownsXmlTypes> tbls_DropdownsXmlTypes { get; set; }
        public virtual DbSet<tbls_Dropdownvalues> tbls_Dropdownvalues { get; set; }
        public virtual DbSet<tbls_Eventlog> tbls_Eventlog { get; set; }
        public virtual DbSet<tbls_EventTypes> tbls_EventTypes { get; set; }
        public virtual DbSet<tbls_FailedLogins> tbls_FailedLogins { get; set; }
        public virtual DbSet<tbls_Groups> tbls_Groups { get; set; }
        public virtual DbSet<tbls_GroupsTypes> tbls_GroupsTypes { get; set; }
        public virtual DbSet<tbls_GroupsXML> tbls_GroupsXML { get; set; }
        public virtual DbSet<tbls_ImportJobs> tbls_ImportJobs { get; set; }
        public virtual DbSet<tbls_ImportLog> tbls_ImportLog { get; set; }
        public virtual DbSet<tbls_Importxml> tbls_Importxml { get; set; }
        public virtual DbSet<tbls_ImportXmlLanguages> tbls_ImportXmlLanguages { get; set; }
        public virtual DbSet<tbls_Languages> tbls_Languages { get; set; }
        public virtual DbSet<tbls_Modules> tbls_Modules { get; set; }
        public virtual DbSet<tbls_ModulesDefinitions> tbls_ModulesDefinitions { get; set; }
        public virtual DbSet<tbls_ModulesPermissions> tbls_ModulesPermissions { get; set; }
        public virtual DbSet<tbls_Pages> tbls_Pages { get; set; }
        public virtual DbSet<tbls_Permissions> tbls_Permissions { get; set; }
        public virtual DbSet<tbls_Roles> tbls_Roles { get; set; }
        public virtual DbSet<tbls_SecurityLevels> tbls_SecurityLevels { get; set; }
        public virtual DbSet<tbls_SystemSettings> tbls_SystemSettings { get; set; }
        public virtual DbSet<tbls_Users> tbls_Users { get; set; }
        public virtual DbSet<tbls_UsersXML> tbls_UsersXML { get; set; }
        public virtual DbSet<tbls_Xmlfields> tbls_Xmlfields { get; set; }
        public virtual DbSet<tbls_xmlfreetextfieldsedit> tbls_xmlfreetextfieldsedit { get; set; }
        public virtual DbSet<tbls_Xmllanguagefieldsedit> tbls_Xmllanguagefieldsedit { get; set; }
        public virtual DbSet<tbls_Xmlsearchfieldsedit> tbls_Xmlsearchfieldsedit { get; set; }
        public virtual DbSet<tbls_Xmltypes> tbls_Xmltypes { get; set; }
        public virtual DbSet<Valuta> Valutas { get; set; }
        public virtual DbSet<Vestiging> Vestigings { get; set; }
        public virtual DbSet<VestigingOpeningHour> VestigingOpeningHours { get; set; }
        public virtual DbSet<tbl_Destinations> tbl_Destinations { get; set; }
        public virtual DbSet<tbl_log4net> tbl_log4net { get; set; }
        public virtual DbSet<tbls_Cache> tbls_Cache { get; set; }
        public virtual DbSet<tbls_PermissionsRoles> tbls_PermissionsRoles { get; set; }
        public virtual DbSet<tbls_Sessions> tbls_Sessions { get; set; }
        public virtual DbSet<tbls_Xmlfieldsancestors> tbls_Xmlfieldsancestors { get; set; }
        public virtual DbSet<CamperAanbiedingMetPrijzen> CamperAanbiedingMetPrijzens { get; set; }
        public virtual DbSet<vboekingen> vboekingens { get; set; }
    
        [DbFunction("busjehurenEntities", "CamperAanbiedingPrijzen")]
        public virtual IQueryable<CamperAanbiedingPrijzen_Result> CamperAanbiedingPrijzen(Nullable<int> camperAanbiedingId, Nullable<int> vestigingId, Nullable<int> aantalVolwassenen, Nullable<int> aantalKinderen, Nullable<System.DateTime> ophaalDatum, Nullable<System.DateTime> terugbrengDatum, Nullable<System.DateTime> reserveringsDatum)
        {
            var camperAanbiedingIdParameter = camperAanbiedingId.HasValue ?
                new ObjectParameter("CamperAanbiedingId", camperAanbiedingId) :
                new ObjectParameter("CamperAanbiedingId", typeof(int));
    
            var vestigingIdParameter = vestigingId.HasValue ?
                new ObjectParameter("vestigingId", vestigingId) :
                new ObjectParameter("vestigingId", typeof(int));
    
            var aantalVolwassenenParameter = aantalVolwassenen.HasValue ?
                new ObjectParameter("AantalVolwassenen", aantalVolwassenen) :
                new ObjectParameter("AantalVolwassenen", typeof(int));
    
            var aantalKinderenParameter = aantalKinderen.HasValue ?
                new ObjectParameter("AantalKinderen", aantalKinderen) :
                new ObjectParameter("AantalKinderen", typeof(int));
    
            var ophaalDatumParameter = ophaalDatum.HasValue ?
                new ObjectParameter("OphaalDatum", ophaalDatum) :
                new ObjectParameter("OphaalDatum", typeof(System.DateTime));
    
            var terugbrengDatumParameter = terugbrengDatum.HasValue ?
                new ObjectParameter("TerugbrengDatum", terugbrengDatum) :
                new ObjectParameter("TerugbrengDatum", typeof(System.DateTime));
    
            var reserveringsDatumParameter = reserveringsDatum.HasValue ?
                new ObjectParameter("ReserveringsDatum", reserveringsDatum) :
                new ObjectParameter("ReserveringsDatum", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<CamperAanbiedingPrijzen_Result>("[busjehurenEntities].[CamperAanbiedingPrijzen](@CamperAanbiedingId, @vestigingId, @AantalVolwassenen, @AantalKinderen, @OphaalDatum, @TerugbrengDatum, @ReserveringsDatum)", camperAanbiedingIdParameter, vestigingIdParameter, aantalVolwassenenParameter, aantalKinderenParameter, ophaalDatumParameter, terugbrengDatumParameter, reserveringsDatumParameter);
        }
    
        [DbFunction("busjehurenEntities", "CamperAanbiedingPrijzen_backup")]
        public virtual IQueryable<CamperAanbiedingPrijzen_backup_Result> CamperAanbiedingPrijzen_backup(Nullable<int> camperAanbiedingId, Nullable<int> vestigingId, Nullable<int> aantalVolwassenen, Nullable<int> aantalKinderen, Nullable<System.DateTime> ophaalDatum, Nullable<System.DateTime> terugbrengDatum)
        {
            var camperAanbiedingIdParameter = camperAanbiedingId.HasValue ?
                new ObjectParameter("CamperAanbiedingId", camperAanbiedingId) :
                new ObjectParameter("CamperAanbiedingId", typeof(int));
    
            var vestigingIdParameter = vestigingId.HasValue ?
                new ObjectParameter("vestigingId", vestigingId) :
                new ObjectParameter("vestigingId", typeof(int));
    
            var aantalVolwassenenParameter = aantalVolwassenen.HasValue ?
                new ObjectParameter("AantalVolwassenen", aantalVolwassenen) :
                new ObjectParameter("AantalVolwassenen", typeof(int));
    
            var aantalKinderenParameter = aantalKinderen.HasValue ?
                new ObjectParameter("AantalKinderen", aantalKinderen) :
                new ObjectParameter("AantalKinderen", typeof(int));
    
            var ophaalDatumParameter = ophaalDatum.HasValue ?
                new ObjectParameter("OphaalDatum", ophaalDatum) :
                new ObjectParameter("OphaalDatum", typeof(System.DateTime));
    
            var terugbrengDatumParameter = terugbrengDatum.HasValue ?
                new ObjectParameter("TerugbrengDatum", terugbrengDatum) :
                new ObjectParameter("TerugbrengDatum", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<CamperAanbiedingPrijzen_backup_Result>("[busjehurenEntities].[CamperAanbiedingPrijzen_backup](@CamperAanbiedingId, @vestigingId, @AantalVolwassenen, @AantalKinderen, @OphaalDatum, @TerugbrengDatum)", camperAanbiedingIdParameter, vestigingIdParameter, aantalVolwassenenParameter, aantalKinderenParameter, ophaalDatumParameter, terugbrengDatumParameter);
        }
    
        [DbFunction("busjehurenEntities", "dualvalue_list")]
        public virtual IQueryable<dualvalue_list_Result> dualvalue_list(string list, string delimiter1, string delimiter2)
        {
            var listParameter = list != null ?
                new ObjectParameter("list", list) :
                new ObjectParameter("list", typeof(string));
    
            var delimiter1Parameter = delimiter1 != null ?
                new ObjectParameter("delimiter1", delimiter1) :
                new ObjectParameter("delimiter1", typeof(string));
    
            var delimiter2Parameter = delimiter2 != null ?
                new ObjectParameter("delimiter2", delimiter2) :
                new ObjectParameter("delimiter2", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<dualvalue_list_Result>("[busjehurenEntities].[dualvalue_list](@list, @delimiter1, @delimiter2)", listParameter, delimiter1Parameter, delimiter2Parameter);
        }
    
        [DbFunction("busjehurenEntities", "GetEigenschappenVoorZoekresultaat")]
        public virtual IQueryable<GetEigenschappenVoorZoekresultaat_Result> GetEigenschappenVoorZoekresultaat(Nullable<int> bestemmingId, Nullable<int> aantalVolwassenen, Nullable<int> aantalKinderen, Nullable<System.DateTime> ophaalDatum, Nullable<int> inleverBestemmingId, Nullable<System.DateTime> terugbrengDatum, string aanbiedingListCSV)
        {
            var bestemmingIdParameter = bestemmingId.HasValue ?
                new ObjectParameter("BestemmingId", bestemmingId) :
                new ObjectParameter("BestemmingId", typeof(int));
    
            var aantalVolwassenenParameter = aantalVolwassenen.HasValue ?
                new ObjectParameter("AantalVolwassenen", aantalVolwassenen) :
                new ObjectParameter("AantalVolwassenen", typeof(int));
    
            var aantalKinderenParameter = aantalKinderen.HasValue ?
                new ObjectParameter("AantalKinderen", aantalKinderen) :
                new ObjectParameter("AantalKinderen", typeof(int));
    
            var ophaalDatumParameter = ophaalDatum.HasValue ?
                new ObjectParameter("OphaalDatum", ophaalDatum) :
                new ObjectParameter("OphaalDatum", typeof(System.DateTime));
    
            var inleverBestemmingIdParameter = inleverBestemmingId.HasValue ?
                new ObjectParameter("InleverBestemmingId", inleverBestemmingId) :
                new ObjectParameter("InleverBestemmingId", typeof(int));
    
            var terugbrengDatumParameter = terugbrengDatum.HasValue ?
                new ObjectParameter("TerugbrengDatum", terugbrengDatum) :
                new ObjectParameter("TerugbrengDatum", typeof(System.DateTime));
    
            var aanbiedingListCSVParameter = aanbiedingListCSV != null ?
                new ObjectParameter("AanbiedingListCSV", aanbiedingListCSV) :
                new ObjectParameter("AanbiedingListCSV", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetEigenschappenVoorZoekresultaat_Result>("[busjehurenEntities].[GetEigenschappenVoorZoekresultaat](@BestemmingId, @AantalVolwassenen, @AantalKinderen, @OphaalDatum, @InleverBestemmingId, @TerugbrengDatum, @AanbiedingListCSV)", bestemmingIdParameter, aantalVolwassenenParameter, aantalKinderenParameter, ophaalDatumParameter, inleverBestemmingIdParameter, terugbrengDatumParameter, aanbiedingListCSVParameter);
        }
    
        [DbFunction("busjehurenEntities", "GetEigenschappenVoorZoekresultaat_bk")]
        public virtual IQueryable<GetEigenschappenVoorZoekresultaat_bk_Result> GetEigenschappenVoorZoekresultaat_bk(Nullable<int> bestemmingId, Nullable<int> aantalVolwassenen, Nullable<int> aantalKinderen, Nullable<System.DateTime> ophaalDatum, Nullable<System.DateTime> terugbrengDatum, string aanbiedingListCSV)
        {
            var bestemmingIdParameter = bestemmingId.HasValue ?
                new ObjectParameter("BestemmingId", bestemmingId) :
                new ObjectParameter("BestemmingId", typeof(int));
    
            var aantalVolwassenenParameter = aantalVolwassenen.HasValue ?
                new ObjectParameter("AantalVolwassenen", aantalVolwassenen) :
                new ObjectParameter("AantalVolwassenen", typeof(int));
    
            var aantalKinderenParameter = aantalKinderen.HasValue ?
                new ObjectParameter("AantalKinderen", aantalKinderen) :
                new ObjectParameter("AantalKinderen", typeof(int));
    
            var ophaalDatumParameter = ophaalDatum.HasValue ?
                new ObjectParameter("OphaalDatum", ophaalDatum) :
                new ObjectParameter("OphaalDatum", typeof(System.DateTime));
    
            var terugbrengDatumParameter = terugbrengDatum.HasValue ?
                new ObjectParameter("TerugbrengDatum", terugbrengDatum) :
                new ObjectParameter("TerugbrengDatum", typeof(System.DateTime));
    
            var aanbiedingListCSVParameter = aanbiedingListCSV != null ?
                new ObjectParameter("AanbiedingListCSV", aanbiedingListCSV) :
                new ObjectParameter("AanbiedingListCSV", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetEigenschappenVoorZoekresultaat_bk_Result>("[busjehurenEntities].[GetEigenschappenVoorZoekresultaat_bk](@BestemmingId, @AantalVolwassenen, @AantalKinderen, @OphaalDatum, @TerugbrengDatum, @AanbiedingListCSV)", bestemmingIdParameter, aantalVolwassenenParameter, aantalKinderenParameter, ophaalDatumParameter, terugbrengDatumParameter, aanbiedingListCSVParameter);
        }
    
        [DbFunction("busjehurenEntities", "GetPeriodsWithoutGaps")]
        public virtual IQueryable<GetPeriodsWithoutGaps_Result> GetPeriodsWithoutGaps(Nullable<int> camperIdParam, Nullable<System.DateTime> pickupDateParam, Nullable<System.DateTime> returnDateParam, Nullable<int> vestigingId)
        {
            var camperIdParamParameter = camperIdParam.HasValue ?
                new ObjectParameter("CamperIdParam", camperIdParam) :
                new ObjectParameter("CamperIdParam", typeof(int));
    
            var pickupDateParamParameter = pickupDateParam.HasValue ?
                new ObjectParameter("PickupDateParam", pickupDateParam) :
                new ObjectParameter("PickupDateParam", typeof(System.DateTime));
    
            var returnDateParamParameter = returnDateParam.HasValue ?
                new ObjectParameter("ReturnDateParam", returnDateParam) :
                new ObjectParameter("ReturnDateParam", typeof(System.DateTime));
    
            var vestigingIdParameter = vestigingId.HasValue ?
                new ObjectParameter("VestigingId", vestigingId) :
                new ObjectParameter("VestigingId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetPeriodsWithoutGaps_Result>("[busjehurenEntities].[GetPeriodsWithoutGaps](@CamperIdParam, @PickupDateParam, @ReturnDateParam, @VestigingId)", camperIdParamParameter, pickupDateParamParameter, returnDateParamParameter, vestigingIdParameter);
        }
    
        [DbFunction("busjehurenEntities", "GetPrijsInformatie")]
        public virtual IQueryable<GetPrijsInformatie_Result> GetPrijsInformatie(Nullable<int> bestemmingId, Nullable<int> aantalVolwassenen, Nullable<int> aantalKinderen, Nullable<System.DateTime> ophaalDatum, Nullable<System.DateTime> terugbrengDatum, Nullable<System.DateTime> reserveringsDatum, Nullable<int> inleverBestemmingId)
        {
            var bestemmingIdParameter = bestemmingId.HasValue ?
                new ObjectParameter("BestemmingId", bestemmingId) :
                new ObjectParameter("BestemmingId", typeof(int));
    
            var aantalVolwassenenParameter = aantalVolwassenen.HasValue ?
                new ObjectParameter("AantalVolwassenen", aantalVolwassenen) :
                new ObjectParameter("AantalVolwassenen", typeof(int));
    
            var aantalKinderenParameter = aantalKinderen.HasValue ?
                new ObjectParameter("AantalKinderen", aantalKinderen) :
                new ObjectParameter("AantalKinderen", typeof(int));
    
            var ophaalDatumParameter = ophaalDatum.HasValue ?
                new ObjectParameter("OphaalDatum", ophaalDatum) :
                new ObjectParameter("OphaalDatum", typeof(System.DateTime));
    
            var terugbrengDatumParameter = terugbrengDatum.HasValue ?
                new ObjectParameter("TerugbrengDatum", terugbrengDatum) :
                new ObjectParameter("TerugbrengDatum", typeof(System.DateTime));
    
            var reserveringsDatumParameter = reserveringsDatum.HasValue ?
                new ObjectParameter("ReserveringsDatum", reserveringsDatum) :
                new ObjectParameter("ReserveringsDatum", typeof(System.DateTime));
    
            var inleverBestemmingIdParameter = inleverBestemmingId.HasValue ?
                new ObjectParameter("InleverBestemmingId", inleverBestemmingId) :
                new ObjectParameter("InleverBestemmingId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetPrijsInformatie_Result>("[busjehurenEntities].[GetPrijsInformatie](@BestemmingId, @AantalVolwassenen, @AantalKinderen, @OphaalDatum, @TerugbrengDatum, @ReserveringsDatum, @InleverBestemmingId)", bestemmingIdParameter, aantalVolwassenenParameter, aantalKinderenParameter, ophaalDatumParameter, terugbrengDatumParameter, reserveringsDatumParameter, inleverBestemmingIdParameter);
        }
    
        [DbFunction("busjehurenEntities", "GetPrijsInformatie_bk")]
        public virtual IQueryable<GetPrijsInformatie_bk_Result> GetPrijsInformatie_bk(Nullable<int> bestemmingId, Nullable<int> aantalVolwassenen, Nullable<int> aantalKinderen, Nullable<System.DateTime> ophaalDatum, Nullable<System.DateTime> terugbrengDatum, Nullable<System.DateTime> reserveringsDatum)
        {
            var bestemmingIdParameter = bestemmingId.HasValue ?
                new ObjectParameter("BestemmingId", bestemmingId) :
                new ObjectParameter("BestemmingId", typeof(int));
    
            var aantalVolwassenenParameter = aantalVolwassenen.HasValue ?
                new ObjectParameter("AantalVolwassenen", aantalVolwassenen) :
                new ObjectParameter("AantalVolwassenen", typeof(int));
    
            var aantalKinderenParameter = aantalKinderen.HasValue ?
                new ObjectParameter("AantalKinderen", aantalKinderen) :
                new ObjectParameter("AantalKinderen", typeof(int));
    
            var ophaalDatumParameter = ophaalDatum.HasValue ?
                new ObjectParameter("OphaalDatum", ophaalDatum) :
                new ObjectParameter("OphaalDatum", typeof(System.DateTime));
    
            var terugbrengDatumParameter = terugbrengDatum.HasValue ?
                new ObjectParameter("TerugbrengDatum", terugbrengDatum) :
                new ObjectParameter("TerugbrengDatum", typeof(System.DateTime));
    
            var reserveringsDatumParameter = reserveringsDatum.HasValue ?
                new ObjectParameter("ReserveringsDatum", reserveringsDatum) :
                new ObjectParameter("ReserveringsDatum", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetPrijsInformatie_bk_Result>("[busjehurenEntities].[GetPrijsInformatie_bk](@BestemmingId, @AantalVolwassenen, @AantalKinderen, @OphaalDatum, @TerugbrengDatum, @ReserveringsDatum)", bestemmingIdParameter, aantalVolwassenenParameter, aantalKinderenParameter, ophaalDatumParameter, terugbrengDatumParameter, reserveringsDatumParameter);
        }
    
        [DbFunction("busjehurenEntities", "GetPrijsInformatieBestGeboekt")]
        public virtual IQueryable<GetPrijsInformatieBestGeboekt_Result> GetPrijsInformatieBestGeboekt(Nullable<int> bestemmingId, Nullable<System.DateTime> ophaalDatumNa)
        {
            var bestemmingIdParameter = bestemmingId.HasValue ?
                new ObjectParameter("BestemmingId", bestemmingId) :
                new ObjectParameter("BestemmingId", typeof(int));
    
            var ophaalDatumNaParameter = ophaalDatumNa.HasValue ?
                new ObjectParameter("OphaalDatumNa", ophaalDatumNa) :
                new ObjectParameter("OphaalDatumNa", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetPrijsInformatieBestGeboekt_Result>("[busjehurenEntities].[GetPrijsInformatieBestGeboekt](@BestemmingId, @OphaalDatumNa)", bestemmingIdParameter, ophaalDatumNaParameter);
        }
    
        [DbFunction("busjehurenEntities", "value_list")]
        public virtual IQueryable<value_list_Result> value_list(string list, string delimiter)
        {
            var listParameter = list != null ?
                new ObjectParameter("list", list) :
                new ObjectParameter("list", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("delimiter", delimiter) :
                new ObjectParameter("delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<value_list_Result>("[busjehurenEntities].[value_list](@list, @delimiter)", listParameter, delimiterParameter);
        }
    }
}
