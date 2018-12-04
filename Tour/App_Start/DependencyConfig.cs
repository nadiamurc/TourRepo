using Autofac;
using Autofac.Integration.Mvc;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DAL;
using System.Data.Entity;
using System.Web.Mvc;
using Tour.Controllers;

namespace Tour
{
    public static class DependencyConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            // Register your MVC controllers. (MvcApplication is the name of the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ApplicationDataBaseContext>().As<DbContext>();
            builder.RegisterType<LocationTypeRepository>().As<ILocationTypeRepository>();
          //  builder.RegisterType<UserAdminController>();
      
            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // OPTIONAL: Enable action method parameter injection (RARE).
            //builder.InjectActionInvoker();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }

   
}