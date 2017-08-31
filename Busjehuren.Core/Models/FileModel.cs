using Busjehuren.Core.EF;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Busjehuren.Core.Models
{
    //Table Bestand
    [Serializable]
    [XmlRoot("Bestand")]
    public class FileModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("Naam")]
        public string Name { get; set; }
        [XmlElement("Omschrijving")]
        public string Description { get; set; }
        [XmlElement("MediaType")]
        public string MediaType { get; set; }
        [XmlElement("LokatieOpSchijf")]
        public string LocationOnDisc { get; set; }
        [XmlElement("LokatieOpWeb")]
        public string LocationOnWeb { get; set; }
        public static FileModel Parse(Bestand bestandEntity)
        {
            if (bestandEntity == null)
                return null;

            var bestand = new FileModel
            {
                Id = bestandEntity.Id,
                LocationOnDisc = bestandEntity.LokatieOpSchijf,
                LocationOnWeb = bestandEntity.LokatieOpWeb,
                MediaType = bestandEntity.MediaType,
                Name = bestandEntity.Naam,
                Description = bestandEntity.Omschrijving
            };
            return bestand;
        }
    }
}
