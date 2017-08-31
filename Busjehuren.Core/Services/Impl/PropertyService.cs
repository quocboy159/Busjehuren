using AutoMapper;
using Busjehuren.Core.Dto;
using Busjehuren.Core.EF;
using Busjehuren.Core.Models;
using Busjehuren.Core.Services.Contract;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqKit;

namespace Busjehuren.Core.Services.Impl
{
    public class PropertyService : BaseService, IPropertyService
    {
        public PropertyService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
            : base(unitOfWork, mapper, log)
        {

        }

        public List<PropertyModel> GetProperties()
        {
            var properies = new List<PropertyModel>();
            _unitOfWork.EigenschapRepository.FindAll(e => e.Status != 2).ForEach(i =>
            {
                properies.Add(Parse(i));
            });

            return properies;
        }

        private PropertyModel Parse(Eigenschap source)
        {
            var item = new PropertyModel();
            item.Id = source.Id;
            item.Name = source.Naam;
            item.PropertyDetails = new List<PropertyDetailModel>();
            source.EigenschapWaardes.ForEach(i =>
            {
                item.PropertyDetails.Add(Parse(i));
            });
            return item;
        }

        private PropertyDetailModel Parse(EigenschapWaarde source)
        {
            var item = new PropertyDetailModel();
            item.Id = source.Id;
            item.PropertyId = source.EigenschapId;
            item.Value = source.Waarde;
            item.Description = source.Bijzonderheden;
            return item;
        }
    }
}
