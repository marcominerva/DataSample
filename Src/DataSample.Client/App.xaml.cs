using DataSample.BusinessLayer.Services;
using DataSample.Client.Views;
using DataSample.DataAccessLayer.Dapper;
using DataSample.DataAccessLayer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace DataSample.Client
{
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = Host
                   .CreateDefaultBuilder()
                   .ConfigureAppConfiguration((context, builder) =>
                   {
                       builder.AddJsonFile("appsettings.local.json", optional: true);
                   })
                   .ConfigureServices((context, services) =>
                   {
                       ConfigureServices(context.Configuration, services);
                   })
                   .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");

            services
                .AddSingleton<IEntityFrameworkContext, EntityFrameworkContext>((_) =>
                {
                    var options = new DbContextOptionsBuilder<EntityFrameworkContext>()
                        .UseSqlServer(connectionString)
                        .Options;

                    return new EntityFrameworkContext(options);
                })
                .AddSingleton<IDapperContext, DapperContext>((_) => new DapperContext(connectionString));

            services.AddSingleton<IProductsService, EntityFrameworkProductsService>();
            //services.AddSingleton<IProductsService, DapperProductsService>();
            //services.AddSingleton<IProductsService, RemoteProductsService>();

            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(EditProductWindow));
        }
    }
}
