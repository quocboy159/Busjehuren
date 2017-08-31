using AutoMapper;
using Busjehuren.Core.Mappers;
using Busjehuren.Core.Services;
using Busjehuren.Core.Services.Contract;
using Busjehuren.Core.Services.Impl;
using log4net;
using Ninject;
using System;

namespace Busjehuren.FE.App_Start
{
    public class NinjectExt
    {
        private IKernel kernel;

        public NinjectExt()
        {
            kernel = new StandardKernel();
            Binding();
            RegisterMappers();
        }

        private static readonly Lazy<NinjectExt> lazy = new Lazy<NinjectExt>(() => new NinjectExt());

        public static NinjectExt Default { get { return lazy.Value; } }

        public IKernel Kernel
        {
            get { return kernel; }
        }

        private void Binding()
        {
            kernel.Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(ctx.Request.ParentContext.Plan.Type.FullName));

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            kernel.Bind<IContentService>().To<ContentService>().InSingletonScope();
        }

        private void RegisterMappers()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ContentMappingProfile());
            });

            kernel.Bind<IMapper>().ToMethod(ctx => config.CreateMapper()).InSingletonScope();
        }

        public T Get<T>(params Ninject.Parameters.IParameter[] parameters)
        {
            return kernel.Get<T>(parameters);
        }
    }
}