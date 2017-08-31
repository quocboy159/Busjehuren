using System.ComponentModel;
using System.Xml.Serialization;
namespace Busjehuren.Core.Enums
{
    public enum DestinationType
    {
        Continent = 1,
        Country = 2,
        City = 3,
        Supplier = 4
    }

    /// <summary>
    /// VerzekeringsType
    /// </summary>
    public enum InsuranceType
    {
        [XmlEnum("Annuleringsverzekering")]
        Cancellation,
        [XmlEnum("Reisverzekering")]
        Travel,
        [XmlEnum("NoRiskverzekering")]
        NoRisk
    }

    public enum BookingType
    {
        [XmlEnum("Onbekend")]
        Unknown = 0,
        [XmlEnum("Reservering")]
        Reservation = 1,
        [XmlEnum("Offerte")]
        Offer = 2,
    }

    public enum Gender
    {
        [Description("Dhr.")]
        m,
        [Description("Mevr.")]
        v
    }

    public enum BusjeType
    {
        Personenbus = 10,
        BestelBusje = 20,
        VerhuisWagen = 30,
    }
}
