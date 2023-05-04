using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Domain.Interfaces
{
    public interface ICarWorkshopServiceRepository
    {
        Task Create(CarWorkshopService cwService);
        Task<IEnumerable<CarWorkshopService>> GetAllByEncodedName(string encodedName);
    }
}
