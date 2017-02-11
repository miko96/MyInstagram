﻿using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using MyInstagram.Domain.Abstract;
using MyInstagram.Domain.Entities;
using MyInstagram.Domain.Concrete;
using MyInstagram.Service.Services;
using MyInstagram.Data.Infrastructure;
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
        {            // put bindings here
            kernel.Bind<IArticleService>().To<ArticleService>();
            kernel.Bind<MyInstagram.Data.Repository.IArticleRepository>()
                .To<MyInstagram.Data.Repository.ArticleRepository>();
            kernel.Bind<DbContext>().To<MyInstagramEntities>().InRequestScope()
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings);
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            //kernel.Bind<IArticleRepository>().To<ArticleRepository>();
        }
    }
}