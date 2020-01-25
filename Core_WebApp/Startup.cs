using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_WebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Core_WebApp.Services;

namespace Core_WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Registers objects in Dependency 
        /// 1. Database Context
        ///     EF Core DbContext
        /// 2. MVC Options
        ///     Filters
        ///     Formatters
        /// 3. Security
        ///     Authenticatoin for Users
        ///     Authorizations
        ///         Based on Roles
        ///                Role Based Policies
        ///         Based on JSON Web Token    
        /// 4. Cookies
        /// 5. CORS Policies
        ///     WEB APIS
        /// 6. Custom Services
        ///     Domain Based Service class aka Business Logic
        /// 7. Sessions
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddControllers(); // WEB API
            services.AddControllersWithViews(); // MVC Request and WEB API Request Processing
            
            // register the DbContext in DI Container
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AppDbConnection"));
            });

            // register repository services in the DI Container
            services.AddScoped<IRepository<Category,int>,CategoryRepository>();
            services.AddScoped<IRepository<Product, int>,ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// IApplicationBuilder --> Used to Manage HttpRequest  using 'Middlewares'
        /// IWebHostEnvironment --> Detect the Hosting env. for execution
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // detect the env. 
            if (env.IsDevelopment())
            {
                // Standard Framework error Page
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // standard excpetion Handler
                app.UseExceptionHandler("/Home/Error");
            }
            // by default uses the wwwroot to read static files 
            // e.g. .js/.css/.img or anu other custom static files
            // to render in Http Response
            app.UseStaticFiles();

            // routing for API and MVC based on EndPoints
            app.UseRouting();
            // used for USerName/PWD and JWT
            app.UseAuthorization();
            // exposes Endpoint ffrom Server to accept Http Request 
            // process it using Routing and generate response 
            app.UseEndpoints(endpoints =>
            {
                // MVC
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
