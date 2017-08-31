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
    public class DestinationDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string DisplayName { get; set; }
        public List<DestinationDto> Cities { get; set; }
    }
}
