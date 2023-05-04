using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshopService.Queries
{
    public class GetCarWorkshopServicesQueryHandler
        : IRequestHandler<GetCarWorkshopServicesQuery, IEnumerable<CarWorkshopServiceDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICarWorkshopServiceRepository _serviceRepository;

        public GetCarWorkshopServicesQueryHandler(ICarWorkshopServiceRepository serviceRepository, IMapper mapper)
        {
            _mapper = mapper;
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<CarWorkshopServiceDto>> Handle(GetCarWorkshopServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetAllByEncodedName(request.EncodedName);
            return _mapper.Map<IEnumerable<CarWorkshopServiceDto>>(services);
        }
    }
}
