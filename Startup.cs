using File_Sharing.Data;
using File_Sharing.Data.DBModels;
using File_Sharing.Services.EmailService.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace File_Sharing
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
            services.AddHttpContextAccessor();

            services.AddScoped<IEmailService, SendContactEmail>();
            services.AddScoped<IEmailService, SendConfirmationEmail>();
            services.AddTransient<IUploadServices, UploadServices>();

            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(
                                    options => options.UseSqlServer(
                                        Configuration.GetConnectionString("DBConnection")
                                        ));

            services.AddIdentity
                    <AppUserExtender, IdentityRole>(
                        options => options.SignIn.RequireConfirmedEmail = true
                    )
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication();
                   

            services.AddAutoMapper(typeof(Startup));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}"); // 404

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                 endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                 

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
