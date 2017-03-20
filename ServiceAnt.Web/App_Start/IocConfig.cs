using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using ServiceAnt.DataAccess;
using ServiceAnt.DataAccess.Context;
using ServiceAnt.DataAccess.Interface;
using ServiceAnt.DataAccess.Repository;
using ServiceAnt.Logic.Api.Factory;
using ServiceAnt.Logic.Api.Service;
using ServiceAnt.Logic.ServicesManagement;
using ServiceAnt.Web.Providers;

namespace ServiceAnt.Web
{
   public class IocConfig
   {
      public static void ConfigureContainer(HttpConfiguration httpConfiguration)
      {
         var builder = new ContainerBuilder();
         RegisterMvcAndWebApiResolvers(builder);
         RegisterDataAccess(builder);
         RegisterServices(builder);
         RegisterOthers(builder);

         ConfigureDependencyResolvers(builder, httpConfiguration);
      }

      private static void RegisterOthers(ContainerBuilder builder)
      {
         builder.RegisterType<CachingMachineFactory>()
            .As<IMachineFactory>()
            .InstancePerRequest();
      }

      private static void RegisterServices(ContainerBuilder builder)
      {
         builder.RegisterAssemblyTypes(typeof (IService).Assembly)
            .Where(type => type.Name.EndsWith("Service") && type.Name.EndsWith("ServicesCommonService") == false)
            .AsImplementedInterfaces()
            .InstancePerRequest();

         builder.RegisterType<IdentityService>()
            .As<IIdentityService>()
            .InstancePerRequest();
      }

      private static void RegisterDataAccess(ContainerBuilder builder)
      {
         builder.RegisterType<ServiceAntContext>()
            .InstancePerLifetimeScope();

         builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerRequest();

         builder.RegisterAssemblyTypes(typeof (RepositoryBase<>).Assembly)
            .Where(type => type.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerRequest();
      }

      private static void RegisterMvcAndWebApiResolvers(ContainerBuilder builder)
      {
         builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();
         builder.RegisterControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();
      }

      // TODO po co to wszystko, jak to działa? jak żyć?
      private static void ConfigureDependencyResolvers(ContainerBuilder builder, HttpConfiguration httpConfiguration)
      {
         var container = builder.Build();
         var webApiResolver = new AutofacWebApiDependencyResolver(container);
         httpConfiguration.DependencyResolver = webApiResolver;

         var mvcResolver = new AutofacDependencyResolver(container);
         DependencyResolver.SetResolver(mvcResolver);
      }
   }
}