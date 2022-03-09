using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuctionCore.Data.Settings;
using AuctionCore.Data.Repositories;
using AuctionCore.Data.Services;
using AuctionCore.Utils.Extensions;

namespace AuctionCore
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
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN"); 
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => 
                options.UseMemberCasing());
            services.AddDistributedMemoryCache();
            services.AddSingleton(sp => new SessionRepository(
                new SessionDatabaseSettings() { 
                    Hostname = "127.0.0.1", Port = 6379, Expires = TimeSpan.FromSeconds(1800) }));
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton(sp => new AuctionRepository(
                new AuctionDatabaseSettings() { 
                    ConnectionString = "mongodb://127.0.0.1:27017", DatabaseName = "auctionDb", CollectionName = "auctions" }));
            services.AddSingleton<IAuctionService, AuctionService>();
            services.AddSingleton(sp => new CategoryRepository(
                new CategoryDatabaseSettings() { 
                    ConnectionString = "mongodb://127.0.0.1:27017", DatabaseName = "auctionDb", CollectionName = "categories" }));
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton(sp => new UserRepository(
                new UserDatabaseSettings() { 
                    ConnectionString = "mongodb://127.0.0.1:27017", DatabaseName = "auctionDb", CollectionName = "users" }));
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMySession();
			app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

	public class Person
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string Name { get; set; }

		public int Age { get; set; }

		public override string ToString()
		{
			return string.Format(
					"Id: %s, Name: %s, Age: %d", Id, Name, Age
				);
		}
	}
}
