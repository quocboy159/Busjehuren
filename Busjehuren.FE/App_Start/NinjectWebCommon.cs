[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Busjehuren.FE.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Busjehuren.FE.App_Start.NinjectWebCommon), "Stop")]

namespace Busjehuren.FE.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using log4net;
    using Busjehuren.Core.Services;
    using Busjehuren.Core.EF;
    using Busjehuren.Core.Services.Contract;
    using Busjehuren.Core.Services.Impl;
    using AutoMapper;
    using Busjehuren.Core.Mappers;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                RegisterMappers(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(ctx.Request.ParentContext.Plan.Type.FullName));

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            kernel.Bind<BusjehurenDbContext>().ToSelf().InRequestScope().OnActivation((Action<BusjehurenDbContext>)(ctx =>
            {
                ctx.Database.Log = m => System.Diagnostics.Trace.Write(m);
            }));

            kernel.Bind<ILocationService>().To<LocationService>();
            kernel.Bind<IContentService>().To<ContentService>();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<ICheckOutService>().To<CheckOutService>();
            kernel.Bind<IPropertyService>().To<PropertyService>();
            kernel.Bind<IEmailService>().To<EmailService>();
            kernel.Bind<IGenericService>().To<GenericService>();
        }

        private static void RegisterMappers(IKernel kernel)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LocationMappingProfile());
                cfg.AddProfile(new ContentMappingProfile());
                cfg.AddProfile(new ProductMappingProfile());
            });

            kernel.Bind<IMapper>().ToMethod(ctx => config.CreateMapper()).InSingletonScope();
        }
    }
}
