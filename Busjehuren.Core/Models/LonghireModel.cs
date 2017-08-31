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
    [XmlRoot("Longhire")]
    public class LonghireModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Days")]
        public int? Days { get; set; }

        [XmlElement("Percentage")]
        public decimal? Percentage { get; set; }

        [XmlElement("LeverancierId")]
        public int? LeverancierId { get; set; }

        public static LonghireModel Parse(Longhire longhireEntity)
        {
            var longhire = new LonghireModel()
            {
                Id = longhireEntity.Id,
                Days = longhireEntity.Days,
                Percentage = longhireEntity.Percentage,
                LeverancierId = longhireEntity.LeverancierId
            };
            return longhire;
        }

        public static List<LonghireModel> Parse(IEnumerable<Longhire> longhireEntity)
        {
            return longhireEntity.Select(Parse).ToList();
        }
    }
}
