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
    public class ContentMappingProfile : Profile
    {
       protected override void Configure()
       {
           CreateMap<Content, ContentDto>();

           CreateMap<Content, ContentModel>();
       }
    }
}
