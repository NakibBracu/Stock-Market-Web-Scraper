using Autofac.Extensions.DependencyInjection;
using Autofac;
using CSEData.Worker;
using Serilog.Events;
using Serilog;
using WorkerService.Application;
using WorkerService.Infrastructure;
using WorkerService.Persistence;
using WorkerService.Application.Features.Training.Services;
using WorkerService.Infrastructure.Features.Services;
using Microsoft.EntityFrameworkCore;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

var migrationAssemblyName = typeof(Worker).Assembly.FullName;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    Log.Information("Application Starting up");

    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .UseSerilog()
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new PersistenceModule(connectionString,
                migrationAssemblyName));
            builder.RegisterModule(new WorkerModule());
        })
        .ConfigureServices(services =>
        {
            // Add DbContext using the connection string and migration assembly
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationAssemblyName)));

            services.AddHostedService<Worker>();
            // Register your service interfaces and implementations
            services.AddSingleton<ICompanyService, CompanyService>();
            services.AddSingleton<IPriceService, PriceService>();
        })
        .Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
