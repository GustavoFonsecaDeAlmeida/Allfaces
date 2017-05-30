using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RedeSocial.API.Models;
using RedeSocial.Data;

namespace RedeSocial.API.Controllers

{
    [RoutePrefix("api/Imagems")]
    public class ImagemsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("GetAll")]
        // GET: api/Imagems
        public IQueryable<Imagem> GetImagem()
        {
            return db.Imagem;
        }

        // GET: api/Imagems/5
        [ResponseType(typeof(Imagem))]
        public IHttpActionResult GetImagem(int id)
        {
            Imagem imagem = db.Imagem.Find(id);
            if (imagem == null)
            {
                return NotFound();
            }

            return Ok(imagem);
        }

        // PUT: api/Imagems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutImagem(int id, Imagem imagem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != imagem.id)
            {
                return BadRequest();
            }

            db.Entry(imagem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("Create")]
        [ResponseType(typeof(Imagem))]
        public IHttpActionResult uploadImagem(string idusuario , string emailusuario , string foto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Imagem imagem = new Imagem()
            {
                idUsuario = idusuario,
                emailUsuario = emailusuario,
                foto = foto


            };

            db.Imagem.Add(imagem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = imagem.id }, imagem);
        }

        // DELETE: api/Imagems/5
        [ResponseType(typeof(Imagem))]
        public IHttpActionResult DeleteImagem(int id)
        {
            Imagem imagem = db.Imagem.Find(id);
            if (imagem == null)
            {
                return NotFound();
            }

            db.Imagem.Remove(imagem);
            db.SaveChanges();

            return Ok(imagem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImagemExists(int id)
        {
            return db.Imagem.Count(e => e.id == id) > 0;
        }
    }
}