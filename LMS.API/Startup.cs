using LMS.Core.IRepo;
using LMS.Data.Data;
using LMS.Data.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LMS.API
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


            services.AddControllers(opt =>
            {
                opt.ReturnHttpNotAcceptable = true;
                opt.SuppressAsyncSuffixInActionNames = false;
            })
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LMS.API", Version = "v1" });
            });

            services.AddDbContext<LMSAPIContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LMSAPIContext")));

            services.AddScoped<IWorkUnit, WorkUnit>();

            services.AddAutoMapper(typeof(MapperProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LMS.API v1"));
                
            }
            else
            {
                //to hide the details in the production mode
                app.UseExceptionHandler(appBuilder =>
                appBuilder.Run(async context =>
               {
                   context.Response.StatusCode = 500;
                   await context.Response.WriteAsync("An unexpected fault happend. Try again later");
               }));
            }

          

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
