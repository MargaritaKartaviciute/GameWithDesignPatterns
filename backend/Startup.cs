using backend.Data;
using backend.Helpers;
using backend.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace backend
{
    public class Startup
    {
        public static string ConnectionString = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SalaContext>(item => {
                Console.WriteLine("Test connection");
                Console.WriteLine(Configuration.GetConnectionString("DefaultConnection"));
                item.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

                });
            services.ConfigureDependencyInjections();
            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(MyAllowSpecificOrigins,
            //    builder =>
            //    {
            //        builder.WithOrigins("http://localhost:3000").AllowAnyOrigin();
            //    });
            //});
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseMvcWithDefaultRoute();
            app.UseCors(
                           options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader()
                       );
            //app.UseHttpsRedirection();
            app.UseSignalR(routes =>
            {
                routes.MapHub<PlayerHub>("/playerHub");
                routes.MapHub<MapHub>("/mapHub");
            });
            app.UseMvc();
        }
    }
}
