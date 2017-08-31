using System.Web.Mvc;
using Busjehuren.FE.Models;
using Busjehuren.Core.Services.Contract;

namespace Busjehuren.FE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly IContentService _contentService;

        public HomeController(ILocationService locationService, IContentService contentService)
        {
            this._locationService = locationService;
            this._contentService = contentService;
        }

        public ActionResult Index()
        {
            var model = new HomeVM();
            model.ContentModel = _contentService.GetByUrlName(GlobalStatic.UrlNameHome);
            model.PromoModel = _contentService.GetByUrlName(GlobalStatic.UrlNameProMoHomePage);
            model.Destinations = _locationService.GetAllDestination();

            return View(model);
        }

        [Route("over-ons")]
        [OutputCache(Duration = 60, VaryByParam="none")]
        public ActionResult AboutUs()
        {
            var model = _contentService.GetByAlias(GlobalStatic.AliasAboutUs);
            return View(model);
        }

        //[Route("test")]
        //public ActionResult test()
        //{
        //    string formatItem = "<a href='/{0}/{1}'>{2}</a>";
        //    var items = new Dictionary<string, string>();
        //    var list1 = new  Dictionary<string, string>();

        //    var list = _locationService.GetAllDestination();

        //    foreach (var item in list)
        //   {
        //       foreach (var city in item.Cities)
        //       {
        //           items.Add(city.DisplayName, string.Format(formatItem, item.UrlName, city.UrlName, city.DisplayName));
        //       }
        //   }

        //    var xxx1 = items.OrderBy(st => st.Key, StringComparer.Ordinal).ToList();

        //    string result = string.Join(" - ", xxx1.Select(x => x.Value));

        //    return new EmptyResult();
        //}


    }
}