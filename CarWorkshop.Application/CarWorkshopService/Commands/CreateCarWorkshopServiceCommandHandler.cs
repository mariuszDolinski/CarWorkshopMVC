using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopService.Commands
{
    internal class CreateCarWorkshopServiceCommandHandler : IRequestHandler<CreateCarWorkshopServiceCommand>
    {
        private readonly ICarWorkshopRepository _carWorkshopRepository;
        private readonly IUserContext _userContext;
        private readonly ICarWorkshopServiceRepository _serviceRepository;

        public CreateCarWorkshopServiceCommandHandler(ICarWorkshopRepository carWorkshopRepository, 
            IUserContext userContext, ICarWorkshopServiceRepository serviceRepository)
        {
            _carWorkshopRepository = carWorkshopRepository;
            _userContext = userContext;
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(CreateCarWorkshopServiceCommand request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carWorkshopRepository.GetByEncodedName(request.CarWorkshopEncodedName!);
            
            var user = _userContext.GetCurrentUser();
            var hasAccess = user != null && carWorkshop.CreatedById == user.Id;

            if (!hasAccess)
            {
                return;
            }

            var carWorkshopService = new Domain.Entities.CarWorkshopService()
            {
                Cost = request.Cost,
                Description = request.Description,
                CarWorkshopId = carWorkshop.Id
            };

            await _serviceRepository.Create(carWorkshopService);
        }
    }
}
