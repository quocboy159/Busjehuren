using Busjehuren.Core.Entity;

namespace Busjehuren.Core.EF
{
    public partial class CamperBestanden : IEntity
    {
        public int Id { get { return CamperId; } } // this is not used
    }
}
