using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Interfaces;
using infrastructure.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        private static object opt;

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<StoreContext>(opt => 
{
    opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
});

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            }
         );

            return services;
        }
    }
}