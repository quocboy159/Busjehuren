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
using System.Web.Mvc;

namespace Busjehuren.Core.Services.Impl
{
    public class ContentService : BaseService, IContentService
    {
        public ContentService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
            : base(unitOfWork, mapper, log)
        {

        }

        public ContentModel GetById(int Id)
        {
            return _mapper.Map<ContentModel>(this._unitOfWork.ContentRepository.Find(Id));
        }

        public ContentModel GetByAlias(string pageAlias, string languageCode = "nl")
        {
            IQueryable<Content> contents = this._unitOfWork.ContentRepository.FindAll(c => c.TypeId == (int)ContentType.ContentTypes.StaticPage && c.Alias == pageAlias);
            var currentContent = contents.FirstOrDefault(c => c.Language.Code.ToLower() == languageCode.ToLower());

            return _mapper.Map<ContentModel>(currentContent);
        }

        public List<ContentModel> GetAllNews(int pageSize = 6, int page = 1, string languageCode = "nl")
        {
            var contents = this._unitOfWork.ContentRepository
                                           .FindAll(c => c.TypeId == (int)ContentType.ContentTypes.News 
                                                           && c.Alias.Equals("nieuws") 
                                                           && c.Language.Code.ToLower() == languageCode.ToLower());

            contents = contents.OrderByDescending(o => o.LastEdited).Skip(pageSize * (page - 1)).Take(pageSize);

            return _mapper.Map<List<ContentModel>>(contents.ToList());
        }

        public ContentModel GetByUrlName(string urlName = "faq", int language = 1)
        {
            return _mapper.Map<ContentModel>(this._unitOfWork.ContentRepository.FindAll(x => x.LanguageId == language && x.UrlName.Trim().Equals(urlName)).FirstOrDefault());
        }

        public List<FaqModel> Faqs(int top = 0, int language = 1, string determineTag = "")
        {
            ContentModel content = GetByUrlName("faq", language);
            var list = new List<string>();
            var faqs = new List<FaqModel>();
            var indexes = content.Text.AllIndexesOf("<" + determineTag + ">");

            for (int i = 0; i < indexes.Count; i++)
            {
                if (i == indexes.Count - 1)
                {
                    list.Add(content.Text.Substring(indexes[i]));
                }
                else
                {
                    list.Add(content.Text.Substring(indexes[i], indexes[i + 1] - indexes[i]));
                }
            }

            list = top == 0 || top > list.Count ? list : list.Take(top).ToList();

            list.ForEach(x =>
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(x);
                var h3tags = doc.DocumentNode.SelectSingleNode(determineTag);
                HtmlNodeCollection vals = doc.DocumentNode.SelectNodes("p | ul");

                faqs.Add(new FaqModel() { Title = h3tags.InnerText, Content = string.Join("", vals.Select(n => n.OuterHtml)) });
            });

            return faqs;
        }

        public List<SelectListItem> LandBestemmings(string key, string topText = "")
        {
            var list = _unitOfWork
                            .context.tbls_Dropdowns
                                    .FirstOrDefault(x => x.strDropDownName.Equals(key)).tbls_Dropdownvalues
                                    .Where(x => !string.IsNullOrEmpty(x.strValue))
                                    .Select(x => x.strValue).ToList() ?? new List<string>();

            if (!string.IsNullOrEmpty(topText))
            {
                var objTop = list.FirstOrDefault(x => x.Equals(topText));
                if (!string.IsNullOrEmpty(objTop))
                {
                    list.Remove(objTop);
                    list.Insert(0, objTop);
                }
            }

            List<SelectListItem> items = new List<SelectListItem>();
            list.ForEach(l =>
            {
                items.Add(new SelectListItem()
                {
                    Value = l,
                    Text = l,
                });
            });

            return items;
        }

        public List<ContentModel> GetAllStaticPages(int language = 1)
        {
            var list = _unitOfWork.ContentRepository
                                    .FindAll(x => x.TypeId == (int)ContentType.ContentTypes.StaticPage)
                                    .ToList()
                                    .Select(x => _mapper.Map<ContentModel>(x)).ToList();

            return list;
        }
    }
}
