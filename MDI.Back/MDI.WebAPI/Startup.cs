using MDI.Logica;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;

namespace MDI.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Obtener la cadena de conexión desde appsettings.json
            string connectionString = Configuration.GetConnectionString("PostgreSQLConnection");
            Console.WriteLine("Connection String: " + connectionString);
            // Configuración del servicio de base de datos utilizando Entity Framework
            services.AddDbContext<MDIContext>(options => options.UseNpgsql(connectionString));

            // Agregar servicios MVC
            services.AddControllers();

            // Configuración de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
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




//using MDI.Logica;
//using Microsoft.EntityFrameworkCore;

//namespace MDI.WebAPI
//{
//    public class Startup
//    {
//        private IConfiguration Configuration { get; }

//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            // Obtener la cadena de conexión desde appsettings.json
//            string connectionString = Configuration.GetConnectionString("PostgreSQLConnection");
//            Console.WriteLine("Connection String: " + connectionString);
//            // Configuración del servicio de base de datos utilizando Entity Framework
//            services.AddDbContext<MDI.Logica.MDIContext>(options => options.UseNpgsql(connectionString));

//        }


//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            // Configuraciones adicionales...

//            app.UseRouting();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });
//        }
//    }
//}
