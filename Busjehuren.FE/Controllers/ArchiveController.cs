using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Busjehuren.Core.Models;
using Busjehuren.Core.Services.Contract;

namespace Busjehuren.FE.Controllers
{
    public class ArchiveController : BaseController
    {
        private IContentService _contentService;

        public ArchiveController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [Route("nieuws")]
        public ActionResult Index()
        {
            List<ContentModel> newsList = _contentService.GetAllNews();
            return View(newsList);
        }

        [Route("ArchiveLoadMore")]
        public ActionResult _LoadMore(int page)
        {
            List<ContentModel> newsRelateds = _contentService.GetAllNews(6,page);
            return PartialView(newsRelateds);
        }

        public ActionResult _NewsRelated()
        {
            List<ContentModel> newsRelateds = _contentService.GetAllNews(3);
            return PartialView(newsRelateds);
        }
    }
}