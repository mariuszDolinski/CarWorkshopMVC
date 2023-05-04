using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistence
{
    public class CarWorkshopDBContext : IdentityDbContext
    {
        public DbSet<Domain.Entities.CarWorkshop> CarWorkshops { get; set; }
        public DbSet<Domain.Entities.CarWorkshopService> Services { get; set; }

        //tworzymy konstruktor aby móc przekazać w dfinicji metody rozszerzającej connectionString
        //patrz Infrastructure.Extensions/ServiceCollectionExtension
        //teraz przy migracji jako default startup project musi być ustawiony MVC
        //i musi tam być zainstalowane Entity Framework
        public CarWorkshopDBContext(DbContextOptions<CarWorkshopDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//potrzebne aby identitydbcontext mogła skonfigurować tabele userów

            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .OwnsOne(c => c.ContactDetails);//pole ContactDetails nie będzie oddzielną tabelą w bazie danych
            
            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .HasMany(c => c.Services)
                .WithOne(s => s.CarWorkshop)
                .HasForeignKey(s => s.CarWorkshopId);
        
        }
    }
}
