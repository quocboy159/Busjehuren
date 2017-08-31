using Busjehuren.Core.Dto;
using Busjehuren.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Busjehuren.Core.Services.Contract
{
    public interface IContentService
    {
        ContentModel GetById(int Id);
        ContentModel GetByUrlName(string urlName = "faq", int language = 1);
        List<FaqModel> Faqs(int top = 0, int language = 1, string determineTag = "");
        ContentModel GetByAlias(string pageAlias, string languageCode = "nl");
        List<SelectListItem> LandBestemmings(string key, string topText = "");
        List<ContentModel> GetAllNews(int pageSize = 6, int page = 1, string languageCode = "nl");
        List<ContentModel> GetAllStaticPages(int language = 1);
    }
}
