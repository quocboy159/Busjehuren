using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.Services
{
    public abstract class BaseService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;
        public ILog _log;

        public BaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public BaseService(IUnitOfWork unitOfWork, ILog log)
            : this(unitOfWork)
        {
            this._log = log;
        }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
            : this(unitOfWork, log)
        {
            this._mapper = mapper;
        }
    }
}
