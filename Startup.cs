using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using T2RMSWS.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace T2RMSWS
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
            //services.AddCors();
            //services.AddControllers();

            //registers dbcontext as a service
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            //this registers identity as service
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>() //include roles in the identity service
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //configure identity
            services.Configure<IdentityOptions>(options => 
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                });

            //JWT Token Authentication for React Native App
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        ValidAudience = Configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //    };
            //});

            //configure strongly typed settings objects
            //var jwtSection = Configuration.GetSection("JwtBearerTokenSettings");
            //services.Configure<JwtBearerTokenSettings>(jwtSection);
            //var jwtBearerToken settings = jwtSection

            //services.AddControllersWithViews();
            //services.AddRazorPages();

            //services.AddMvc();
            //MvcOptions.EnableEndpointRouting = false;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //"{area:exists}/{controller=Home}/{action=Index}/{id?}"

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

            //invoke the create roles method
            createUserRoles(services).Wait();

            //enable JWT authentication
            //app.UseAuthentication();
            //app.UseMvc();
        }

        //--Add to assign Roles
        private async Task createUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Manager", "Member", "Staff" };

            foreach(var r in roles)
            {
                bool exists = await roleManager.RoleExistsAsync(r);
                if (!exists)
                {
                    var role = new IdentityRole(r);
                    await roleManager.CreateAsync(role);
                }
            }
        }

    }

    //JWT settings 
    public class JwtBearerTokenSettings
    {
        //Check and fill out these properties
        public string Issuer { get { return ""; } }
        public string Audience { get { return ""; } }
    }
}
