using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using Busjehuren.Core.Models;
using Busjehuren.Core.Services.Contract;
using Busjehuren.FE.Models;
using Busjehuren.Common.Utils;

namespace Busjehuren.FE.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContentService _contentService;
        private readonly IEmailService _emailService;

        public ContactController(IContentService contentService, IEmailService emailService)
            : base()
        {
            this._contentService = contentService;
            this._emailService = emailService;
        }

        [Route("contact")]
        public ActionResult Index()
        {
            var model = new ContactVM();
            model.Content = _contentService.GetByAlias("contact");
            return View(model);
        }

        [Route("contact")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactVM model)
        {
            _emailService.Send(new EmailModel()
            {
                To = new string[] { GlobalStatic.ToEmailContact },
                From = model.Email,
                Subject = GlobalStatic.SubjectContact,
                Body = RazorHelper.Parse(GlobalStatic.EmailTemplatesContact, model)
            });

            ViewData["message_send"] = "Sent Your messsage";

            model.Content = _contentService.GetByAlias("contact");

            return View(model);
        }
    }
}