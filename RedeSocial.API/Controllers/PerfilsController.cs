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
    [RoutePrefix("api/Perfils")]
    public class PerfilsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Perfils
        public IQueryable<Perfil> GetPerfil()
        {
            return db.Perfil;
        }

        // GET: api/Perfils/5
        [Route("SelecionarPerfil")]
        [ResponseType(typeof(Perfil))]
        public IHttpActionResult GetPerfil(string id)
        {
            //var perfil = db.Perfil.Select(a => a.idUsuario == id);
            var perfil = db.Perfil.Where(a => a.idUsuario == id);
          
            var perfil2 = perfil.FirstOrDefault<Perfil>();

            if (perfil2 != null)
            {
                return Ok(perfil2);
            }

            return BadRequest();
        }

       

        // POST: api/Perfils
        [Route("Create")]
        [ResponseType(typeof(Perfil))]
        public IHttpActionResult PostPerfil(Perfil perfil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Perfil.Add(perfil);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = perfil.id }, perfil);
        }

        // POST: api/Perfils
        [Route("Update")]
        [ResponseType(typeof(Perfil))]
        public IHttpActionResult UpdatePerfil(Perfil perfil )
        { 

            var PegandoPerfil = db.Perfil.Where(a => a.idUsuario == perfil.idUsuario);

            var objperfil = PegandoPerfil.FirstOrDefault<Perfil>();

            objperfil.idUsuario = perfil.idUsuario;
            objperfil.emailUsuario = perfil.emailUsuario;
            objperfil.PrimeiroNome = perfil.PrimeiroNome;
            objperfil.UltimoNome = perfil.UltimoNome;
            objperfil.fotoPerfil = perfil.fotoPerfil;
            objperfil.Apelido = perfil.Apelido;
            db.SaveChanges();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            return CreatedAtRoute("DefaultApi", new { id = perfil.id }, perfil);
        }





        // Get: api/Perfils
        [Route("VerificaPerfil")]
        [ResponseType(typeof(Perfil))]
        public IHttpActionResult perfilExiste(string id)
        {

            var perfil = db.Perfil.Where(a => a.idUsuario == null);

            if (perfil == null)
            {
                return BadRequest();

            }


            return Ok();
        }



        // DELETE: api/Perfils/5
        [ResponseType(typeof(Perfil))]
        public IHttpActionResult DeletePerfil(int id)
        {
            Perfil perfil = db.Perfil.Find(id);
            if (perfil == null)
            {
                return NotFound();
            }

            db.Perfil.Remove(perfil);
            db.SaveChanges();

            return Ok(perfil);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PerfilExists(int id)
        {
            return db.Perfil.Count(e => e.id == id) > 0;
        }
    }
}