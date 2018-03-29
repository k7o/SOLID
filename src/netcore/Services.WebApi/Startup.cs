using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services.WebApi.Controllers;
using Services.WebApi.Conventions;
using Services.WebApi.FeatureProviders;
using Services.WebApi.ModelBinders;
using Services.WebApi.Swagger.Filters;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;

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
                .AddMvc(options =>
                {
                    options.Conventions.Add(new FromBodyRequiredConvention());
                    options.ModelBinderProviders.Insert(0, new QueryModelBinderProvider());
                })
                .AddJsonOptions(json =>
                {
                    json.SerializerSettings.Formatting = Formatting.Indented;
                    json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
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
                    p.FeatureProviders.Add(new QueryControllerFeatureProvider());
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.OperationFilter<BodyRequiredOperationFilter>();
                c.SchemaFilter<CommandSchemaDefaultExampleFilter>();
            });

            IntegrateSimpleInjector(services);
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitializeContainer(app, loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            container.Verify();

            app.UseMvcWithDefaultRoute();
        }

        private void InitializeContainer(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Add application presentation components:
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);

            Bootstrapper.Bootstrap(container);

            container.Register<ILogger>(() => loggerFactory.CreateLogger("test"));
            container.RegisterInstance(loggerFactory);
            

            container.AutoCrossWireAspNetComponents(app);
        }
    }
}
