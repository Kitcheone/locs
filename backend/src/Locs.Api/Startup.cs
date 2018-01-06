namespace Locs.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using FluentValidation.AspNetCore;
    using Locs.Api.Database;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using NJsonSchema;
    using NPoco;
    using NSwag.AspNetCore;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add configuration service
            services.AddSingleton<IConfiguration>(this.Configuration);

            // Add framework services.
            services.AddMvc();

            // Configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAll",
                    corsBuilder =>
                    {
                        corsBuilder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

            // Npoco setup
            LocsDatabaseFactory.Setup(this.Configuration);

            // Autofac setup
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsImplementedInterfaces();

            builder.Register(c => LocsDatabaseFactory.DbFactory.GetDatabase())
                .As<IDatabase>()
                .InstancePerDependency();

            builder.Populate(services);
            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("AllowAll");
            app.UseMvc();
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, new SwaggerUiSettings() { DefaultPropertyNameHandling = PropertyNameHandling.CamelCase });
        }
    }
}
