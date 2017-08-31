using AutoMapper;
using Busjehuren.Core.Dto;
using Busjehuren.Core.Services.Contract;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Services.Impl
{
    public class AdreService : BaseService, IAdreService
    {
        public AdreService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
            : base(unitOfWork, mapper, log)
        {

        }

        public IList<AdreDto> Getlist()
        {
            var list = this._unitOfWork.AdreRepository.FindAll().Take(10).Select(_mapper.Map<AdreDto>).ToList();
            return list;
        }

        public AdreDto GetById(int Id)
        {
            return _mapper.Map<AdreDto>(this._unitOfWork.AdreRepository.Find(Id));
        }
    }
}
