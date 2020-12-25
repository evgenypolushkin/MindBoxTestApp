using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MindboxTestApp.Core;
using MindboxTestApp.Core.Mappings;
using MindboxTestApp.Core.Services;
using MindboxTestApp.Middlewares;

namespace MindboxTestApp
{
    /// <summary>
    ///     Startup
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly string _connectionString;

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }

        /// <summary>
        ///     Application configuration
        /// </summary>
        public IConfiguration Configuration { get; }


        /// <summary>
        ///     Configuring services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSwaggerGen(i =>
            {
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                i.IncludeXmlComments(xmlPath);
            });
            services.AddControllers().AddNewtonsoftJson();
            services.AddScoped<IFigureService, FigureService>();
            services.AddTransient<ErrorHandlingMiddleware>();
            services.AddScoped<IApplicationDbContextFactory, ApplicationDbContextFactory>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(_connectionString));
        }

        private static void MigrateDataBase(IApplicationDbContextFactory dbContextFactory)
        {
            using ApplicationDbContext dbContext = dbContextFactory.Create();
            dbContext.Database.MigrateAsync();
        }


        /// <summary>
        ///     Configuring application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="dbContextFactory"></param>
        /// <param name="figureService"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApplicationDbContextFactory dbContextFactory, IFigureService figureService)
        {
            MigrateDataBase(dbContextFactory);
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger(i => i.SerializeAsV2 = true);
            app.UseSwaggerUI(i => { i.SwaggerEndpoint("/swagger/v1/swagger.json", "Mindbox Test Application API"); });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            FigureMappings.ConfigureMapsterMappings(figureService);
        }
    }
}