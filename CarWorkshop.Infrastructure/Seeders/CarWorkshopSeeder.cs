using CarWorkshop.Infrastructure.Persistence;

namespace CarWorkshop.Infrastructure.Seeders
{
    public class CarWorkshopSeeder
    {
        private CarWorkshopDBContext _dbContext;
        public CarWorkshopSeeder(CarWorkshopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync())
            {
                if(!_dbContext.CarWorkshops.Any())
                {
                    var mazdaASO = new Domain.Entities.CarWorkshop()
                    {
                        Name = "Mazda ASO",
                        Description = "Autoryzowany serwis Mazda",
                        ContactDetails = new()
                        {
                            City = "Kraków",
                            Street = "Szewska 2",
                            PostalCode = "30-001",
                            PhoneNumber = "+48699222888"
                        }
                    };
                    mazdaASO.EncodeName();

                    _dbContext.CarWorkshops.Add(mazdaASO);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
