using AutoMapper;
using Busjehuren.Core.Dto;
using Busjehuren.Core.Services.Contract;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Busjehuren.Common.Extensions;
using System.Xml.Linq;
using System.Web;
using Busjehuren.Core.EF;
using HtmlAgilityPack;
using Busjehuren.Core.Models;

namespace Busjehuren.Core.Services.Impl
{
    public class ContactService : BaseService, IContactService
    {
        public ContactService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
            : base(unitOfWork, mapper, log)
        {

        }
    }
}
