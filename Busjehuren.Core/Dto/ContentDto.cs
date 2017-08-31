using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class ContentDto
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int? DestinationId { get; set; }
        public int TypeId { get; set; }
        public string DisplayName { get; set; }
        public string UrlName { get; set; }
        public string HeadText { get; set; }
        public string Text { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public DateTime? LastEdited { get; set; }
        public string EditedBy { get; set; }
        public string Alias { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? ZoomLevel { get; set; }
        
    }
}
