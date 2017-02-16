using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using MyInstagram.Data;
using System.Configuration;
using Ninject.Web.Common;

namespace MyInstagram.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam) { kernel = kernelParam; AddBindings(); }

        public object GetService(Type serviceType) { return kernel.TryGet(serviceType); }

        public IEnumerable<object> GetServices(Type serviceType) { return kernel.GetAll(serviceType); }

        private void AddBindings()
        {         
            kernel.Bind<DbContext>().To<MyInstagramEntities>().InRequestScope()
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings);          
        }
    }
}