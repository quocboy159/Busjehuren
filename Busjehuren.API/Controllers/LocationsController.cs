using Busjehuren.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Busjehuren.API.Controllers
{
    [RoutePrefix("api/locations")]
    public class LocationsController : ApiController
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService, IContentService contentService)
        {
            this._locationService = locationService;
        }

        [HttpGet]
        [Route("cities")]
        public IHttpActionResult GetAllCity()
        {
            var respond = _locationService.GetAllDestination();

            return Ok(respond);
        }

        
    }
}
