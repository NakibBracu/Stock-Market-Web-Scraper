using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Application.Features.Training.Services;
using WorkerService.Infrastructure.Features.Services;

namespace WorkerService.Infrastructure
{
    public class InfrastructureModule : Module
    {
        public InfrastructureModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CompanyService>().As<ICompanyService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PriceService>().As<IPriceService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
