using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using Applications.PersonnelTracker;
using Applications.PersonnelTracker.Models;
using Applications.Core.Models;
using Applications.PersonnelTracker.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.SpaServices.Webpack;

namespace Applications_PatentCirculation
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
            services.AddMvcCore()
                .AddRazorViewEngine()
                .AddJsonFormatters(opt =>
                {
                    opt.Converters.Add(new StringEnumConverter());
                    opt.NullValueHandling = NullValueHandling.Ignore;
                    opt.DefaultValueHandling = DefaultValueHandling.Ignore;
                });

            services.AddOptions();

            services.Configure<EmailSetting>(Configuration.GetSection("emailSettings"));
            services.Configure<ApplicationSetting>(Configuration.GetSection("applicationSettings"));

            services.AddSingleton(Configuration);

            services.AddDbContext<PersonnelTrackerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Application")));

            var container = new Container();
            container.Configure(config =>
            {
                config.AddRegistry<Applications.Core.Business.StandardRegistry>();
                config.AddRegistry(new StandardRegistry(Configuration));
                config.AddRegistry<AutoMapperRegistry>();

                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });

                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var contextService = serviceScope.ServiceProvider.GetService<PersonnelTrackerContext>();
                    contextService.Database.EnsureDeleted();
                    contextService.Database.EnsureCreated();
                    //contextService.Database.Migrate();
                    contextService.EnsureSeedData();
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }           

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
