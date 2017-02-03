using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sus.Base.Core.Infrastructure;
using Sus.Base.Core.Configuration;
using Sus.Base.Framework;
using Sus.Base.Core;
using Sus.Base.Framework.Mvc.Routes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Sus.Base.Core.Infrastructure.DependencyManagement;
using Microsoft.Extensions.Options;

namespace Sus.Base.Web
{
    public class Startup
    {
        private IHostingEnvironment _env;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _env = env;
            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }
        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddOptions();
            services.AddAuthorization();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));
            services.AddSingleton(_env);
            services.AddMvc();
            StaticResolver.Config(services.BuildServiceProvider());
            EngineContext.Initialize(false,services);
            //this.ApplicationContainer = container;
            return new AutofacServiceProvider(EngineContext.Current.Container.Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,IServiceProvider service,IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            //SusHttpContext.Config(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            //处理静态注入类ServiceProvider的中间件，感觉不是很优雅，等以后大神的解决方案吧
            app.UseMiddleware<StaticResolverMiddleware>();
            StaticResolver.Config(app.ApplicationServices);
            EngineContext.RunStartUpTask(service);
            app.UseApplicationInsightsRequestTelemetry();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookie",
                LoginPath = new PathString("/Login"),
                AccessDeniedPath = new PathString("/Forbidden"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();
            app.UseStaticFiles();
            //app.Use(new Func<RequestDelegate, RequestDelegate>(nextapp => new ContainerMiddleware(nextapp,app.ApplicationServices)))
            app.UseMvc(routes =>
            {
                var rp= service.GetService<IRoutePublisher>();
                rp.RegisterRoutes(routes);
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
    }
}
