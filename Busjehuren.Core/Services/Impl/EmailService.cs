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
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Busjehuren.Common.Extensions;
using System.Xml.Linq;
using System.Web;
using Busjehuren.Core.EF;
using HtmlAgilityPack;
using Busjehuren.Core.Models;
using FluentEmail;

namespace Busjehuren.Core.Services.Impl
{
    public class EmailService : BaseService, IEmailService
    {
        public EmailService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
            : base(unitOfWork, mapper, log)
        {

        }

        public void Send(EmailModel model)
        {
            List<MailAddress> toMailAddresses = new List<MailAddress>();
            if (model.To != null)
            {
                foreach (var to in model.To)
                {
                    toMailAddresses.Add(new MailAddress(to));
                }
            }
            var email = Email.From(model.From)
                .To(toMailAddresses)
                .Subject(model.Subject)
                .Body(model.Body).BodyAsHtml();
            email.Send();
        }
    }
}
