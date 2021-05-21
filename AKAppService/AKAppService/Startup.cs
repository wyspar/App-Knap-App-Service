using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AKAppBL;
using AKAppDL;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;

namespace AKAppService
{
    [ExcludeFromCodeCoverage]
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AKAppService", Version = "v1" });
            });
            services.AddCors(
            options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            services.AddDbContext<AKAppDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AppKnapDB")));
            services.AddScoped<IAppRepoDB, AppRepoDB>();
            services.AddScoped<IAppBL, AppBL>();
            services.AddScoped<IUploadRepoDB, UploadRepoDB>();
            services.AddScoped<IUploadBL, UploadBL>();
            services.AddScoped<ILocationRepoDB, LocationRepoDB>();
            services.AddScoped<ILocationBL, LocationBL>();
            services.AddScoped<BlobServiceClient>(sp => new BlobServiceClient(Configuration.GetConnectionString("BlobStorage")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AKAppService v1"));
            }

            app.UseCors(x =>
                x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

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
