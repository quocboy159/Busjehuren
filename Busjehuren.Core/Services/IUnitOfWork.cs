namespace Busjehuren.Core.Services
{
    using Busjehuren.Core.EF;
    using Busjehuren.Core.Repositories;
    using log4net;
    using System;
    using System.Data.Entity.Validation;
    using System.Security.Principal;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
        BusjehurenDbContext context { get; }

        IAdreRepository AdreRepository { get; }
        IContentRepository ContentRepository { get; }
        IDestinationRepository DestinationRepository { get; }
        ICamperAanbiedingRepository CamperAanbiedingRepository { get; }
        IVestigingRepository VestigingRepository { get; }
        IEigenschapWaardeRepository EigenschapWaardeRepository { get; }
        ICamperRepository CamperRepository { get; }
        IEigenschapRepository EigenschapRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IPersonRepository PersonRepository { get; }
        IReservationPackageRepository ReservationPackageRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private BusjehurenDbContext _context;

        private static readonly ILog log = LogManager.GetLogger(typeof(UnitOfWork));

        public UnitOfWork(BusjehurenDbContext context)
        {
            this._context = context;

            this.AdreRepository = new AdreRepository(_context);
            this.ContentRepository = new ContentRepository(_context);
            this.DestinationRepository = new DestinationRepository(_context);
            this.CamperAanbiedingRepository = new CamperAanbiedingRepository(_context);
            this.VestigingRepository = new VestigingRepository(_context);
            this.EigenschapWaardeRepository = new EigenschapWaardeRepository(_context);
            this.CamperRepository = new CamperRepository(_context);
            this.EigenschapRepository = new EigenschapRepository(_context);
            this.ReservationRepository = new ReservationRepository(_context);
            this.PersonRepository = new PersonRepository(_context);
            this.ReservationPackageRepository = new ReservationPackageRepository(_context);
        }

        public BusjehurenDbContext context 
        {
            get
            {
                return _context;
            }
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (_disposed)
                return;

            _disposed = true;
        }

        #region init Repository

        public IAdreRepository AdreRepository { get; private set; }
        public IContentRepository ContentRepository { get; private set; }
        public IDestinationRepository DestinationRepository { get; private set; }
        public ICamperAanbiedingRepository CamperAanbiedingRepository { get; private set; }
        public IVestigingRepository VestigingRepository { get; private set; }
        public IEigenschapWaardeRepository EigenschapWaardeRepository { get; private set; }
        public ICamperRepository CamperRepository { get; private set; }
        public IEigenschapRepository EigenschapRepository { get; private set; }
        public IReservationRepository ReservationRepository { get; private set; }
        public IPersonRepository PersonRepository { get; private set; }
        public IReservationPackageRepository ReservationPackageRepository { get; private set; }
        #endregion
    }
}