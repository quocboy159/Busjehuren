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
    public class ProductMappingProfile : Profile
    {
       protected override void Configure()
       {
           CreateMap<GetPrijsInformatie_Result, PriceInfoModel>();
           CreateMap<CamperAanbieding, CamperAanbiedingModel>()
               .ForMember(dest => dest.DuurInDagen, opt => opt.MapFrom(p => (int)Math.Ceiling((p.PeriodeTot - p.PeriodeVan).TotalDays)));
       }
    }
}
