using RedeSocial.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedeSocial.API.Controllers
{
    [RoutePrefix("api/Service")]
    public class ServiceController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Service
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET: api/Service/5
        [Route("Verifica")]
        public IHttpActionResult Get(string id)
        {
            var perfil = db.Perfil.Select(a => a.idUsuario == id);

            perfil.FirstOrDefault();
           


            if (perfil.FirstOrDefault() == true)
            {
                return Ok();

            }


            return BadRequest();

        }

        // POST: api/Service
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Service/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Service/5
        public void Delete(int id)
        {
        }
    }
}
