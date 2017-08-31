using Busjehuren.Core.Entity;

namespace Busjehuren.Core.EF
{
    public partial class ContentType : IEntity
    {
        public enum ContentTypes
        {
            Homepage = 1,
            LandingsPage = 2,
            StaticPage = 3,
            SupplierLandingPage = 4,
            News = 5
        }
    }
}
