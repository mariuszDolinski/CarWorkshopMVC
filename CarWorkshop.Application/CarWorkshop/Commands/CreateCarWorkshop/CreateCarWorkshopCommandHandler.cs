﻿using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop
{
    internal class CreateCarWorkshopCommandHandler : IRequestHandler<CreateCarWorkshopCommand>
    {
        private readonly ICarWorkshopRepository _carWorkshopRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateCarWorkshopCommandHandler(ICarWorkshopRepository carWorkshopRepository, 
            IMapper mapper, IUserContext userContext) 
        {
            _carWorkshopRepository = carWorkshopRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task Handle(CreateCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var carWorkshop = _mapper.Map<Domain.Entities.CarWorkshop>(request);
            carWorkshop.EncodeName();

            carWorkshop.CreatedById = _userContext.GetCurrentUser()!.Id;

            await _carWorkshopRepository.Create(carWorkshop);
        }
    }
}
