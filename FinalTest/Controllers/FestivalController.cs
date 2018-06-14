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
    public class FestivalController : ApiController
    {
        IFestivalsRepository _repository { get; set; }

        public FestivalController(IFestivalsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Festival> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var festival = _repository.GetById(id);
            if (festival == null)
            {
                return NotFound();
            }
            return Ok(festival);
        }

        public IHttpActionResult Post(Festival festival)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(festival);
            return CreatedAtRoute("DefaultApi", new { id = festival.Id }, festival);
        }

        public IHttpActionResult Delete(int id)
        {
            var festival = _repository.GetById(id);
            if (festival == null)
            {
                return NotFound();
            }

            _repository.Delete(festival);
            return Ok();
        }

        public IHttpActionResult Put(int id, Festival festival)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != festival.Id)
            {
                return BadRequest();
            }

            _repository.Update(festival);

            return Ok(festival);
        }

        [Route("api/festival/pretraga")]
        public IEnumerable<Festival> PostByPeriod(int start, int kraj)
        {
            return _repository.PostByPeriod(start, kraj);
        }

    }
}
