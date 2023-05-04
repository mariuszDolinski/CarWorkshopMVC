using CarWorkshop.Application.Extensions;
using CarWorkshop.Infrastructure.Extensions;
using CarWorkshop.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarWorkshop.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// dodajemy opcj�, kt�re sprawia, �e pola bez ? nie b�d� domy�lnie podawane walidcji
builder.Services.AddControllersWithViews(option => option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
//dodajemy w�asn� metod� rozszerzaj�c� zdefiniowan� w Infrastructure.Extensions ServiceCollectionExtension
//po to aby w Infrastructure by�o wszystko co zwi�zane z baz� danych (clean architecture)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<CarWorkshopSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();//automayczne przekierowanie na https nawet gdy u�yto http
app.UseStaticFiles();

app.UseRouting();//dopasowuje �cie�ki do konkretnych handler�w

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();//dodajemy obs�ug� RazorPages, bo na tym dzia�a Identity
//dodatkowo trzeba stworzy� plik Areas/.../_ViewStart.cshtml aby razor pages wy�wietla�y si� na layoucie mvc
//w _Layout.cshmtl dodajemu odpowiedni widok do menu przez <partial />
app.Run();
