using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace Clients.WebApi.WebApp
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

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            IntegrateSimpleInjector(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Add Serilog to the logging pipeline
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.MapWhen(context => context.Request.Path.StartsWithSegments(@"/api/command") || 
                                   context.Request.Path.StartsWithSegments(@"/api/query"),
                builder => builder.RunProxy(new ProxyOptions
                {
                    Scheme = "http",
                    Host = "localhost",
                    Port = "51964"
                }));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            InitializeContainer(app, loggerFactory);
            container.Verify();
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));
            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        private void InitializeContainer(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Add application presentation components:
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);

            container.RegisterInstance(loggerFactory);
            container.RegisterInstance(loggerFactory.CreateLogger("test"));

            container.AutoCrossWireAspNetComponents(app);
        }
    }
}
