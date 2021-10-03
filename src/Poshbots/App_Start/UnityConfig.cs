using Microsoft.Practices.Unity;
using Poshbots.Core.Providers;
using Poshbots.Core.Repositories;
using Poshbots.Infrastructure.Providers;
using Poshbots.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poshbots
{
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IBotRepository, BotRepository>();

            container.RegisterType<IAzureFunctionProvider, AzureFunctionProvider>();
            container.RegisterType<IFileIOProvider, FileIOProvider>();
        }
    }
}