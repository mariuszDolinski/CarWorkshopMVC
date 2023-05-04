using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByName
{
    public class GetCarWorkshopByNameQueryHandler : IRequestHandler<GetCarWorkshopByNameQuery, CarWorkshopDto>
    {
        private readonly ICarWorkshopRepository _carWorkshopRepository;
        private readonly IMapper _mapper;

        public GetCarWorkshopByNameQueryHandler(ICarWorkshopRepository carWorkshopRepository, IMapper mapper)
        {
            _carWorkshopRepository = carWorkshopRepository;
            _mapper = mapper;
        }

        public async Task<CarWorkshopDto> Handle(GetCarWorkshopByNameQuery request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carWorkshopRepository.GetByEncodedName(request.EncodedName);
            return _mapper.Map<CarWorkshopDto>(carWorkshop);    
        }
    }
}
