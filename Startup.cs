using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using V3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace V3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuration for services
            // ...
           


            //    // Add services to the container.
            //    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            //    builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlite(connectionString));



            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));


            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                  .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            // You can also add roles if needed: .AddRoles<IdentityRole>()

            // ...


            //services.AddIdentity<IdentityUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
            //      .AddEntityFrameworkStores<ApplicationDbContext>();



            services.AddHttpContextAccessor();

                services.AddControllersWithViews();
            



            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 443; // Set the HTTPS port to 443
            });

            services.AddAuthorization();
            services.AddControllers();
            services.AddRazorPages();
            services.AddAuthentication()
                     .AddFacebook(options =>
                     {
                         options.AppId = "741385531130761";
                         options.AppSecret = "82a37453b71f25030f66549bb8782415";
                         // Additional options can be configured here
                     });




            // Other service configurations
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                // Other middleware configurations
                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();                
                app.UseAuthorization();
                app.UseAuthentication();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "Custom",
                        pattern: "Movie/Random/Id",
                        defaults: new { controller = "Movie", action = "Random" });

                    endpoints.MapControllerRoute(
                        name: "customerRoute",
                        pattern: "Customer/Details/name",
                        defaults: new { controller = "Customer", action = "Details" });

                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });
            }
    }
}


