using Busjehuren.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Busjehuren.FE.Controllers
{
    public class PageController : Controller
    {
        private readonly IContentService _contentService;

        public PageController(IContentService contentService)
        {
            this._contentService = contentService;
        }

        public ActionResult Index(string alias)
        {
            var content = _contentService.GetByAlias(alias);

            return View(content);
        }
    }
}