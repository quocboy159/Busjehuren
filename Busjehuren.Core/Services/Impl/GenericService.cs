using AutoMapper;
using Busjehuren.Core.Dto;
using Busjehuren.Core.Models;
using Busjehuren.Core.Services.Contract;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Services.Impl
{
    public class GenericService : BaseService, IGenericService
    {
        public GenericService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
            : base(unitOfWork, mapper, log)
        {

        }

        public CamperAanbiedingModel GetCamperAanbiedingById(int Id)
        {
            var camperAanbieding = _unitOfWork.CamperAanbiedingRepository.Find(Id);

            return _mapper.Map<CamperAanbiedingModel>(camperAanbieding);
        }
    }
}
