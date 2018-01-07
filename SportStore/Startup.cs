namespace SportsStore
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using SportsStore.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMvc();

            // sesje
            services.AddMemoryCache();
            services.AddSession();
            services.AddScoped<Cart>(SessionCart.GetCart);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseStatusCodePages();

            // sesje
            app.UseSession();
            app.UseMvc(
                routes =>
                    {
                        routes.MapRoute(
                            name: null,
                            template: "{category}/Page{productPage:int}",
                            defaults: new { Controller = "Product", action = "List" });
                        routes.MapRoute(
                            name: null,
                            template: "Page{productPage:int}",
                            defaults: new { controller = "Product", action = "List", productPage = 1 });
                        routes.MapRoute(
                            name: null,
                            template: string.Empty,
                            defaults: new { controller = "Product", action = "List", productPage = 1 });

                        routes.MapRoute(
                            name: null,
                            template: "{category}",
                            defaults: new { controller = "Product", action = "List", productPage = 1 });
                    });
            SeedData.EnsurePopulated(app);
        }
    }
}