using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByName
{
    public class GetCarWorkshopByNameQuery : IRequest<CarWorkshopDto>
    {
        public  string EncodedName { get; set; }

        public GetCarWorkshopByNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
