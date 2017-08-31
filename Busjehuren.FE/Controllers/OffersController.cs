using Busjehuren.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Busjehuren.FE.Controllers
{
    public class OffersController : BaseController
    {
        private readonly IContentService _contentService;
        public OffersController(IContentService contentService)
        {
            this._contentService = contentService;
        }

        [Route("de-aanbiedingen")]
        public ActionResult Index()
        {
            var content = _contentService.GetByAlias("offers");
            return View(content);
        }
	}
}