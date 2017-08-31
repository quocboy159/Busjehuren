using Busjehuren.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Busjehuren.API.Controllers
{
    [RoutePrefix("api/contents")]
    public class ContentsController : ApiController
    {
        private readonly IContentService _contentService;

        public ContentsController(IContentService contentService)
        {
            this._contentService = contentService;
        }

        [HttpGet]
        [Route("{urlName}")]
        public IHttpActionResult GetHome(string urlName = "")
        {
            var respond = _contentService.GetByUrlName(urlName);

            return Ok(respond);
        }

        [HttpGet]
        [Route("faqs/{top}")]
        public IHttpActionResult faqs(int top = 0)
        {
            var respond = _contentService.Faqs(top);

            return Ok();
        }

    }
}
