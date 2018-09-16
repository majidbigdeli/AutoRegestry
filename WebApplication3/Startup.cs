using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StructureMap;
using WebApplication3.service;

namespace WebApplication3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddControllersAsServices();

            //   services.AddScoped<Imajid, majid>();

            var container = new ServiceResolver(services).GetServiceProvider();
            return container;


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

          public IServiceProvider ConfigureIoC(IServiceCollection services)
    {
        var container = new Container();

        container.Configure(config =>
        {
            // Register stuff in container, using the StructureMap APIs...
            config.Scan(_ =>
            {
                _.AssemblyContainingType(typeof(Startup));
                _.WithDefaultConventions();
            });

         //   config.For(typeof(IValidator<>)).Add(typeof(DefaultValidator<>));
         //   config.For(typeof(ILeaderboard<>)).Use(typeof(Leaderboard<>));
         //   config.For<IUnitOfWork>().Use(_ => new UnitOfWork(3)).ContainerScoped();

            //Populate the container using the service collection
            config.Populate(services);
        });

        return container.GetInstance<IServiceProvider>();

    }
    }
}
