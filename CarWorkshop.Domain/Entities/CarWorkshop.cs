using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Domain.Entities
{
    public class CarWorkshop
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;//wykrzyknik mówi kompilatorowi, że to nie null
        public string? Description { get; set; }//? dopuszcza wartość null
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ContactDetails ContactDetails { get; set; } = default!;
        public string? About { get; set; }
        public string? CreatedById { get; set; }//CreatedBy musi być taki sam tu i niżej
        public IdentityUser? CreatedBy { get; set; }//trzeba zainstalować paczkę Identity.Stores

        public List<CarWorkshopService> Services { get; set; } = new(); 

        public string EncodedName { get; private set; } = default!;

        //EncodedName - przechowuje nazwę warsztatu która będzie w pasku adresu
        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");
    }
}
