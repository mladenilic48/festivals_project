using FinalniTest.Interfaces;
using FinalniTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalniTest.Controllers
{
    public class PlaceController : ApiController
    {
        IPlaceRepository _repository { get; set; }

        public PlaceController(IPlaceRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<Place> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //[Route("api/mesta/kod")]
        public IEnumerable<Place> GetByPostCode(int kod)
        {
            return _repository.GetByPostCode(kod);
        }
    }
}
