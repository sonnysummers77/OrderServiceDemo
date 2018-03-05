using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderServiceDemo.Models.Options;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderServiceDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            HostingEnvironment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment HostingEnvironment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var config = Mapping.AutoMapperConfig.Configure();
            services.AddSingleton(config.CreateMapper());

            services.AddSingleton(Configuration);
            services.AddSingleton(HostingEnvironment);
            services.AddResponseCompression();
            services.Configure<OrderServiceDemoOptions>(Configuration);
            services.Configure<OrderServiceDemoOptions>(options => options.Environment = HostingEnvironment.EnvironmentName);

            services.AddMvc().AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(Attributes.HandleErrorAttribute));
            });

            var containerBuilder = new ContainerBuilder();

            var serviceAssembly = Assembly.Load(new AssemblyName("OrderServiceDemo.Services"));
            var webAssembly = Assembly.Load(new AssemblyName("OrderServiceDemo"));
            containerBuilder
                .RegisterAssemblyTypes(serviceAssembly, webAssembly)
                .Where(x => !x.Namespace.StartsWith("OrderServiceDemo.Services.Repositories"))
                .AsImplementedInterfaces();

            var useInMemoryDataStores = Configuration.GetValue("UseInMemoryRepositories", true);
            containerBuilder
                .RegisterAssemblyTypes(serviceAssembly, webAssembly)
                .Where(x => x.Namespace.StartsWith(useInMemoryDataStores ? "OrderServiceDemo.Services.Repositories.InMemoryRepositories" : "OrderServiceDemo.Services.Repositories.SqlRepositories"))
                .AsImplementedInterfaces();

            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseResponseCompression();
            app.UseMvc();

            Task.Run(() =>
            {
                var logger = loggerFactory.CreateLogger(nameof(Startup));
                StringBuilder sbInfo = new StringBuilder();
                sbInfo.AppendLine("Application Configuration: ");
                foreach (var kvp in Configuration.AsEnumerable())
                {
                    sbInfo.AppendLine($"{kvp.Key} = {kvp.Value}");
                }
                logger.LogInformation(sbInfo.ToString());
            });
        }
    }
}
