using Busjehuren.Core.Dto;
using Busjehuren.Core.Services;
using Busjehuren.Core.Services.Contract;
using System.Linq;
using System.Web.Http;

namespace Busjehuren.API.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IAdreService _adreService;
        private readonly IUnitOfWork _unitOfWork;

        public ValuesController(IUnitOfWork unitOfWork, IAdreService adreService)
        {
            this._unitOfWork = unitOfWork;
            this._adreService = adreService;
        }

        // GET api/values
        public IHttpActionResult Get()
        {
            return Ok(_adreService.Getlist().Select(x => x.Id));
        }

        // GET api/values/5
        public AdreDto Get(int id)
        {
            return _adreService.GetById(id);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
