using Busjehuren.Core.Entity;
using System.ComponentModel;

namespace Busjehuren.Core.EF
{
    public partial class Pakket : IEntity
    {
        [DefaultValue(1)]
        public int Number { get; set; }
        public bool IsCheck { get; set; }
    }
}
