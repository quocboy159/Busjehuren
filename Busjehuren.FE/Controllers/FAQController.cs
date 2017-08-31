using System.Web.Mvc;
using Busjehuren.FE.Models;
using Busjehuren.Core.Services.Contract;

namespace Busjehuren.FE.Controllers
{
    public class FAQController : Controller
    {
        private readonly IContentService _contentService;

        public FAQController(ILocationService locationService, IContentService contentService)
        {
            this._contentService = contentService;
        }

        [Route("faq")]
        [OutputCache(Duration = 60, VaryByParam = "none")]
        public ActionResult Index()
        {
           var model = _contentService.GetByUrlName(GlobalStatic.UrlNameFaq);
            return View(model);
        }
    }
}