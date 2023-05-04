using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            //dodajemy mediator podając dowolny typ komendy lub kwerendy
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining(typeof(CreateCarWorkshopCommand)));
            //services.AddAutoMapper(typeof(CarWorkshopMappingProfile));

            //dodanie Automappera z konfiguracją (tak by można było przekazać przez konstruktor
            //   usercontext
            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new CarWorkshopMappingProfile(userContext));
            }).CreateMapper());

            //dodajemy fluent validation i walidację po strnie klienta
            services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()//wystarczy podać jeden dowolny walidator z folderu
                .AddFluentValidationAutoValidation()//zastępujemy domyślną walidację przez fluent
                .AddFluentValidationClientsideAdapters();//dodajemy walidację po stronie klienta
        }
    }
}
