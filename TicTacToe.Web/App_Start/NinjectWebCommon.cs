using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using TicTacToe.Bll.Infrastructure;
using TicTacToe.Bll.Interfaces;
using TicTacToe.Bll.Services;
using TicTacToe.Web;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace TicTacToe.Web
{
    // Generated code. Do not touch
    [GeneratedCode("Ninject", "1.0")]
    [ExcludeFromCodeCoverage]
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            
            Bootstrapper.Initialize(createKernel);

            setupDependencyResolver(GlobalConfiguration.Configuration, Bootstrapper.Kernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel createKernel()
        {
            var modules = new INinjectModule[]
            {
                new ServiceModule("DefaultConnection")
            };

            var kernel = new StandardKernel(modules);

            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => Bootstrapper.Kernel);
            registerServices(kernel);

            return kernel;
        }

        private static void setupDependencyResolver(HttpConfiguration config, IKernel kernel)
        {
            //As Ninject provides its own model validator providers this will left only Ninject's providers to avoid duplicated error model messages.
            config.Services.Clear(typeof(System.Web.Http.Validation.ModelValidatorProvider));
            
            config.DependencyResolver = new NinjectDependencyResolver(kernel);
        }

        private static void registerServices(IKernel kernel)
        {
            kernel.Bind<IPlayerService>().To<PlayerService>();
            kernel.Bind<IGameService>().To<GameService>();
        }
    }
}