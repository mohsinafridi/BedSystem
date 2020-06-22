using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelCloudBedSystem.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using HotelCloudBedSystem.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using HotelCloudBedSystem.Utility;
using Stripe;
using HotelCloudBedSystem.Filteration.HotelFilteration;
using HotelCloudBedSystem.Filteration.CheckOutCheckIn;
using HotelCloudBedSystem.Filteration.RoomFilteration;

namespace HotelCloudBedSystem
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<SignInManager<AppUser>>();
            services.Configure<StripSetting>(Configuration.GetSection("Stripe"));
            services.AddSingleton(Configuration);

            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddDbContext<HotelCloudDbContext>();
            services.AddTransient<IEFRepository, EFRepository>();
            services.AddTransient<IAppUserIEFRepository, AppUserIEFRepository>();
            services.AddIdentity<AppUser, AppRole>(
                Configuration =>
                {
                    Configuration.SignIn.RequireConfirmedEmail = false;
                    Configuration.SignIn.RequireConfirmedPhoneNumber = false;
                })
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<HotelCloudDbContext>()
                .AddDefaultTokenProviders();


            services.Configure<SecurityStampValidatorOptions>(options =>
    options.ValidationInterval = TimeSpan.FromSeconds(10));

            //External Login Authentication...

    //        services.AddAuthentication()
    //  .AddFacebook(facebookOptions =>
    //  {
    //      facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
    //      facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
    //  }).AddGoogle(options =>
    //  {
    //      IConfigurationSection googleAuthNSection =
    //          Configuration.GetSection("Authentication:Google");

    //      options.ClientId = googleAuthNSection["ClientId"];
    //      options.ClientSecret = googleAuthNSection["ClientSecret"];
    //  })
    //.AddMicrosoftAccount(microsoftOptions =>
    //{
    //    microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
    //    microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
    //});




            //Email And Sms Configuration
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.Configure<SMSoptions>(Configuration);


            //Hotel Filteration Configuration
            services.AddTransient<IFilterHotelByHotelRatings, FilterHotelByHotelRatings>();
            services.AddTransient<IFilterHotelByCityAndRoomType, FilterHotelByCityAndRoomType>();
            services.AddTransient<IFilterHotelByPriceAndHotelRatings, FilterHotelByPriceAndHotelRatings>();
            services.AddTransient<IHotelNetworkMainSearchEngine, HotelNetworkMainSearchEngine>();
            services.AddTransient<IFilterHotelByPrice, FilterHotelByPrice>();


            //CheckOut  CheckIn  Configuration
            services.AddTransient<ICheckOutCheckInImplmentation, CheckOutCheckInImplmentation>();


            //HotelRoom Filteration Configuration
            services.AddTransient<IFilterRoomsByHotelId, FilterRoomsByHotelId>();
            services.AddTransient<IFilterRoomsByPrice, FilterRoomsByPrice>();
            services.AddTransient<IFilterRoomsByPriceAndRating, FilterRoomsByPriceAndRating>();
            services.AddTransient<IFilterRoomsByRating, FilterRoomsByRating>();





            services.Configure<IdentityOptions>(Option =>
            {
                Option.User.RequireUniqueEmail = true;
                Option.Password.RequireDigit = false;
                Option.Password.RequiredLength = 8;
                Option.Password.RequireLowercase = false;
                Option.Password.RequireUppercase = false;
                Option.Password.RequiredUniqueChars = 0;
                Option.Password.RequireNonAlphanumeric = false;

                Option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                Option.Lockout.MaxFailedAccessAttempts = 5;
                Option.Lockout.AllowedForNewUsers = true;

                // User settings.
                Option.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                Option.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(config =>
            {
                config.AccessDeniedPath = "/Identity/Account/AccessDenied";
                config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                config.Cookie.HttpOnly = true;
                config.Cookie.Name = "CloudBedCookie";
                config.LoginPath = "/Identity/Account/Login";
                config.LogoutPath = "/SignIn/SignOut";
                config.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                config.SlidingExpiration = true;


            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        //Middle Wear
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            StripeConfiguration.SetApiKey(Configuration["Stripe:SecretKey"]);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "Default",
                  template: "{controller}/{action}/{id?}",
                  defaults: new
                  {
                      controller = "Home",
                      action = "Index"
                  }
                  );

                //Areas (e.g Admin,Manager)
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller}/{action}/{id?}",
                    defaults: new
                    {
                        controller = "Home",
                        action = "Index"

                    }
                    );
            });
        }
    }
}
