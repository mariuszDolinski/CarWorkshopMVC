using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;
using CarWorkshop.Infrastructure.Repositories;
using CarWorkshop.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace CarWorkshop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        //definiujemy metodę rozszerzającą dla serwisu (dlatego jest this, bo na serwisie będzie wywołana)
        //a serwis jest typu IServiceCollection. Dodatkowo dodajemy parametr Iconfiguration aby móc
        //przekazać connection string z appsettings.json poleceniem GetConnectionString
        //option możemy wykozystać dzięki stworzeniu konstruktora w CarWorkshopDbContext
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarWorkshopDBContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("CarWorkshop"));
            });
            //dodajemy komunikację między bazą danych i Identity
            services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Stores.MaxLengthForKeys = 450;//zaiększa długość kluczy w db, zapobigając skracaniu przy migracji
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CarWorkshopDBContext>();
                

            services.AddScoped<CarWorkshopSeeder>();
            services.AddScoped<ICarWorkshopRepository, CarWorkshopRepository>();
            services.AddScoped<ICarWorkshopServiceRepository, CarWorkshopServiceRepository>();
        }
    }
}
