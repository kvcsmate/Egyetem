using Library.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Library.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Library.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DbType dbType = Configuration.GetValue<DbType>("DbType");

            //ez a ami váltogat sqlite és sqlserver között, most megnézte milyen típus van az appsettings-ben
            switch (dbType)
            {
                case DbType.SqlServer:
                    services.AddDbContext<LibraryDbContext>(options =>
                    {
                        options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"));
                    }
                    );

                    break;
                case DbType.Sqlite:
                    services.AddDbContext<LibraryDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));
                    break;
                default:
                    break;
            }
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<LibraryDbContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<ILibraryService,LibraryService>(); 
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            var context = services.GetRequiredService<LibraryDbContext>();
            var directory = Configuration["ImageStore"];
            DbInitializer.Initialize(context, directory); //, directory
        }
    }
}
