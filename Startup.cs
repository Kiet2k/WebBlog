using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using razorweb.Models;

namespace razorweb
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
            services.AddOptions();
            var mailSetting = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailSetting);
            services.AddSingleton<IEmailSender,SendMailService>();
         
            services.AddRazorPages();
            services.AddDbContext<MyblogContext>(options=>
            {
                string connectString = Configuration.GetConnectionString("MyBlogContext");
                options.UseSqlServer(connectString);
            });
            //dang ky identity
            services.AddIdentity<AppUser,IdentityRole>()
                    .AddEntityFrameworkStores<MyblogContext>()
                    .AddDefaultTokenProviders();
            
            // services.AddDefaultIdentity<AppUser>()
            //         .AddEntityFrameworkStores<MyblogContext>()
            //         .AddDefaultTokenProviders();
            
            // Truy cập IdentityOptions
            services.Configure<IdentityOptions> (options => {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = true;        //xác thực tài khoản

            });
            services.ConfigureApplicationCookie(options => {
                options.LoginPath="/login/";
                options.LogoutPath = "/logout/";
                options.AccessDeniedPath="/khongduoctruycap.html";
            });
            services.AddAuthentication()
                        .AddGoogle(options=>
                        {
                            var ggcongfig = Configuration.GetSection("Authentication:Google");

                            options.ClientId=ggcongfig["ClientId"];
                            options.ClientSecret=ggcongfig["ClientSecret"];
                            //https://localhost:5001/dang-nhap-tu-google
                            options.CallbackPath = "/dang-nhap-tu-google";
                        })
                        .AddFacebook(options => 
                        {
                            var fbconfig = Configuration.GetSection("Authentication:Facebook");
                            options.AppId = fbconfig["AppId"];
                            options.AppSecret = fbconfig["AppSecret"];
                            options.CallbackPath = "/dang-nhap-tu-facebook";
                        });
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
                app.UseExceptionHandler("/Error");
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
                endpoints.MapRazorPages();
            });
        }
    }
}
