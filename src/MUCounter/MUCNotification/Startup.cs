using System;
using Hangfire;
using Hangfire.Mongo;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MUCNotification.Application.IntegrationEventHandlers;
using MUCNotification.Application.Services;
using MUCNotification.Infrastructure;

namespace MUCNotification
{
    public class Startup
    {
        private readonly IServiceProvider serviceProvider;

        public Startup(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            this.serviceProvider = serviceProvider;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<RepetitionAddedEventHandler>();
            services.AddMassTransit(c =>
            {
                c.AddConsumer<RepetitionAddedEventHandler>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(
                cfg =>
                {
                    var host = cfg.Host(new Uri(this.Configuration.GetValue<string>("rabbitMQ")), z => { });

                    cfg.ReceiveEndpoint(host, "web-service-endpoint2", e =>
                    {
                        e.PrefetchCount = 16;
                        e.LoadFrom(provider);
                        EndpointConvention.Map<RepetitionAddedEventHandler>(e.InputAddress);
                    });
                }));

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IHostedService, BusService>();
            services.AddSingleton<IMongoClient>(new MongoClient(this.Configuration.GetValue<string>("mongoConnectionString")));
            services.AddTransient<DailyRepsCounterRepository>();
            services.AddTransient<IRaportService, RaportService>();
            services.AddTransient<SmsService>();

            var migrationOptions = new MongoMigrationOptions
            {
                Strategy = MongoMigrationStrategy.Migrate,
                BackupStrategy = MongoBackupStrategy.Collections
            };
            var storageOptions = new MongoStorageOptions
            {
                MigrationOptions = migrationOptions,
                CheckConnection = false

            };
            services.AddHangfire(config =>
            {
                config.UseMongoStorage(this.Configuration.GetValue<string>("mongoConnectionString"), "hangfire", storageOptions);
            });
            services.AddHangfireServer();

        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseHangfireDashboard();
            RecurringJob
                .AddOrUpdate(() => app.ApplicationServices.GetService<IRaportService>().Raport(), Cron.Daily(21, 45), TimeZoneInfo.Local);
        }
    }
}
