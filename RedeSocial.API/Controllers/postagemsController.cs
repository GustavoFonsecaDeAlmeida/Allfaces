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
    
    [RoutePrefix("api/postagems")]
    public class postagemsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/postagems
        [Route("GetAll")]
        public IQueryable<postagem> GetPostagem()
        {
            return db.Postagem;
        }

        // GET: api/postagems/5
        [ResponseType(typeof(postagem))]
        public IHttpActionResult Getpostagem(int id)
        {
            postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return NotFound();
            }

            return Ok(postagem);
        }

        // PUT: api/postagems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpostagem(int id, postagem postagem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postagem.id)
            {
                return BadRequest();
            }

            db.Entry(postagem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!postagemExists(id))
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

        // POST: api/postagems
        [Route("Create")]
        [ResponseType(typeof(postagem))]
        public IHttpActionResult Postpostagem(postagem postagem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Postagem.Add(postagem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = postagem.id }, postagem);
        }

        // DELETE: api/postagems/5
        [Route("Deletar")]
        [ResponseType(typeof(postagem))]
        public IHttpActionResult Deletepostagem(int id)
        {
            postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return NotFound();
            }

            db.Postagem.Remove(postagem);
            db.SaveChanges();

            return Ok(postagem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool postagemExists(int id)
        {
            return db.Postagem.Count(e => e.id == id) > 0;
        }
    }
}