using Busjehuren.Core.Services.Contract;
using Busjehuren.FE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Busjehuren.FE.Controllers
{
    public class ContentDetailController : BaseController
    {
        private readonly IContentService _contentService;

        public ContentDetailController(IContentService contentService)
            : base()
        {
            this._contentService = contentService;
        }

        [HttpGet]
        [Route("nieuws/{title}/{idNews}")]
        public ActionResult Index(int idNews, string title)
        {
            var detailNews = _contentService.GetById(idNews);
            return View(detailNews);
        }

        [HttpGet]
        [Route("GetTermsConditions")]
        public ActionResult GetTermsConditions()
        {
            var content = _contentService.GetByUrlName(GlobalStatic.UrlNameTermsConditions).Text;

            return Json(content, JsonRequestBehavior.AllowGet);
        }
	}
}