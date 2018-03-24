using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services.WebApi.MissingDIExtensions;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using System.Reflection;

namespace Services.WebApi
{
    public class Startup
    {
        readonly Container container = new Container();

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services
                .AddMvc()
                .AddApplicationPart(typeof(CommandController<>).Assembly)
                .ConfigureApplicationPartManager(p =>
                {
                    var dependentLibrary = p.ApplicationParts
                                    .FirstOrDefault(part => part.Name == "DependentLibrary");
                    if (dependentLibrary != null)
                    {
                        p.ApplicationParts.Remove(dependentLibrary);
                    }
                })
                .ConfigureApplicationPartManager(p =>
                {
                    p.FeatureProviders.Add(new CommandControllerFeatureProvider());
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            IntegrateSimpleInjector(services);
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRequestScopingMiddleware(() => AsyncScopedLifestyle.BeginScope(container));
            services.AddCustomControllerActivation(container.GetInstance);
            services.AddCustomViewComponentActivation(container.GetInstance);
            services.AddCustomTagHelperActivation(container.GetInstance, IsApplicationType);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            RegisterApplicationComponents(app, loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvcWithDefaultRoute();
        }

        private void RegisterApplicationComponents(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            Bootstrapper.Bootstrap(container);
            
            container.RegisterSingleton(loggerFactory);

            container.Verify();
        }

        private static bool IsApplicationType(Type type) => type.GetTypeInfo().Namespace.StartsWith("Services.WebApi");
    }
}
