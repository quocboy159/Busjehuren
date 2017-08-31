using Busjehuren.Core.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Busjehuren.Core.Models
{
    [Serializable]
    [XmlRoot("VestigingOpeningHour")]
    public class VestigingOpeningHourModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        //[XmlElement("Vestiging")]
        //public Vestiging Vestiging { get; set; }

        [XmlElement("VestigingId")]
        public int VestigingId { get; set; }

        [XmlElement("Weekday")]
        public int Weekday { get; set; }

        [XmlElement("WeekdayName")]
        public string WeekdayName { get; set; }

        [XmlElement("FromHour")]
        public string FromHour { get; set; }

        [XmlElement("ToHour")]
        public string ToHour { get; set; }

        public static List<VestigingOpeningHourModel> Parse(IEnumerable<VestigingOpeningHour> vestigingOpeningHourEntity)
        {
            return vestigingOpeningHourEntity.Select(p => Parse(p)).ToList();
        }

        public static VestigingOpeningHourModel Parse(VestigingOpeningHour vestigingOpeningHourEntity)
        {
            var vestigingOpeningHour = new VestigingOpeningHourModel
            {
                Id = vestigingOpeningHourEntity.Id,
                VestigingId = vestigingOpeningHourEntity.VestigingId,
                Weekday = vestigingOpeningHourEntity.Weekday,
                WeekdayName = vestigingOpeningHourEntity.WeekdayName,
                FromHour = vestigingOpeningHourEntity.FromHour.Value.ToString("00.00").Replace('.', ':'),
                ToHour = vestigingOpeningHourEntity.ToHour.Value.ToString("00.00").Replace('.', ':')
            };

            return vestigingOpeningHour;
        }
    }
}
