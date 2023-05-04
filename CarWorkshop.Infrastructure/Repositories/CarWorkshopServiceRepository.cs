using CarWorkshop.Domain.Entities;
using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Repositories
{
    public class CarWorkshopServiceRepository : ICarWorkshopServiceRepository
    {
        private readonly CarWorkshopDBContext _dBContext;

        public CarWorkshopServiceRepository(CarWorkshopDBContext dBContext) 
        {
            _dBContext = dBContext;
        }

        public async Task Create(CarWorkshopService cwService)
        {
            _dBContext.Services.Add(cwService);
            await _dBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarWorkshopService>> GetAllByEncodedName(string encodedName)
                => await _dBContext.Services
                    .Where(s => s.CarWorkshop.EncodedName == encodedName)
                    .ToListAsync();
    }
}
