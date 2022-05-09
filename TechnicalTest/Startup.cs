using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TechnicalTest
{
    using BusinessLogic;
    using BusinessLogic.Exceptions;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Repository;
    using Test.EfData;
    using static System.Net.Mime.MediaTypeNames;

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
            services.AddMvc();
            const string connectionString = @"Server=localhost;Database=gnchanger-ermir;Trusted_Connection=True;";
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString, x =>
            {
                x.MigrationsAssembly("Test.EfData");
            }));

            //services.AddSingleton<IRepository, InMemoryRepository>();
            //services.AddSingleton<ICustomersManager, CustomersManager>();

            services.AddScoped<IRepository, EFRepository>();
            services.AddScoped<ICustomersManager, CustomersManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var errorContent = "Opsie... that's a 500";
                    var errorStatusCode = StatusCodes.Status500InternalServerError;
                    var contentType = Text.Plain;

                    var exHandlerFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exHandlerFeature != null
                        && exHandlerFeature.Error != null
                        && exHandlerFeature.Error is DomainException)
                    {
                        var dx = exHandlerFeature.Error as DomainException;
                        errorContent = JsonConvert.SerializeObject(new { Error = dx.DomainMessage });
                        errorStatusCode = StatusCodes.Status422UnprocessableEntity;
                        contentType = "application/json";
                    }

                    context.Response.StatusCode = errorStatusCode;
                    context.Response.ContentType = contentType;
                    await context.Response.WriteAsync(errorContent);
                });
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
