﻿using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using VinculacionBackend.Data.Interfaces;
using VinculacionBackend.Data.Repositories;
using VinculacionBackend.Interfaces;
using VinculacionBackend.Security;
using VinculacionBackend.Services;

namespace VinculacionBackend
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterServices(builder);
            RegisterRepositories(builder);

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver =
            new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerRequest();
            builder.RegisterType<MajorRepository>().As<IMajorRepository>().InstancePerRequest();
            builder.RegisterType<HourRepository>().As<IHourRepository>().InstancePerRequest();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>().InstancePerRequest();
            builder.RegisterType<SendEmail>().As<ISendEmail>().InstancePerRequest();
            builder.RegisterType<Encryption>().As<IEncryption>().InstancePerRequest();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<StudentsServices>().As<IStudentsServices>().InstancePerRequest();
            builder.RegisterType<MajorsServices>().As<IMajorsServices>().InstancePerRequest();
            builder.RegisterType<HoursServices>().As<IHoursServices>().InstancePerRequest();
            builder.RegisterType<ProjectServices>().As<IProjectServices>().InstancePerRequest();
        }
    }
}