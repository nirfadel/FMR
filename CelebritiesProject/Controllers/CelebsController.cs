using Core.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using System.Web.Script.Serialization;

namespace CelebritiesProject.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CelebsController : ApiController
    {
        private readonly ICelebRepository _celebRepository;

        public CelebsController()
        {
            _celebRepository = new CelebRepository();
        }

        [HttpGet]
        public List<Celebrity> GetTop100Celebrities()
        {
            return _celebRepository.GetTop100Celebrities();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCelebById(string Id)
        {
            bool deleted = _celebRepository.DeleteCelebById(Id);
            return Json(new { Deleted = deleted });
        }

        [HttpGet]
        public List<Celebrity> ClearTop100Celebrities()
        {
           return  _celebRepository.ClearTop100Celebrities();
        }
    }
}
