using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Busjehuren.Core.EF;
using Busjehuren.Core.Dto;
using Busjehuren.Core.Models;

namespace Busjehuren.Core.Mappers
{
   public class LocationMappingProfile : Profile
    {
       protected override void Configure()
       {
           CreateMap<Destination, DestinationDto>();
           CreateMap<Destination, DestinationModel>();

           CreateMap<Vestiging, VestigingModel>();
           CreateMap<VestigingOpeningHour, VestigingOpeningHourModel>();
       }
    }
}
