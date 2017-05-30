using Newtonsoft.Json;
using RedeSocial.WEB.Models;
using RedeSocial.WEB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RedeSocial.WEB.Controllers
{
    public class PerfilController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private HttpClient _client;

        public PerfilController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:60551/");
            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }

        ImageService imageService = new ImageService();

        // GET: Perfil
        public ActionResult Index()
        {
            return View();
        }


     
      
        public async Task<ActionResult> Details(string id)
        {
            var uriparameter = "api/Perfils/SelecionarPerfil?id=" + id;


            var response = await _client.GetAsync(uriparameter);

            if (response.IsSuccessStatusCode)
            {
                var JsonString = await response.Content.ReadAsStringAsync();

                var Perfil = JsonConvert.DeserializeObject<PerfilViewModel>(JsonString);


                return View(Perfil);


            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
            
                

        // GET: Perfil/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PerfilViewModel model)

        {
            
            
            

            model.idUsuario = Session["idUsuario"].ToString();
            model.emailUsuario = Session["EmailUsuario"].ToString();
            model.fotoPerfil = "SemFotoPerfil";



            if (ModelState.IsValid)
            {

                var response = await _client.PostAsJsonAsync("api/Perfils/Create", model);

                return RedirectToAction("Index", "Home");

            }
            else
            {
                return View();
            }

        }


        
        [Authorize]
        public async Task<ActionResult> Edit(string id)
        {
            var uriparameter = "api/Perfils/SelecionarPerfil?id=" + id;


            var response = await _client.GetAsync(uriparameter);

            if (response.IsSuccessStatusCode)
            {
                var JsonString = await response.Content.ReadAsStringAsync();

                var Perfil = JsonConvert.DeserializeObject<PerfilViewModel>(JsonString);


                return View(Perfil);


            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Edit(string id ,PerfilViewModel model , HttpPostedFileBase photo)
        {
            var a = model.id;
            var b = model.idUsuario;
            var c = model.PrimeiroNome;
            var d = model.UltimoNome;
            var e = model.fotoPerfil;
            var f = model.Apelido;

            var uploadImagem = await imageService.UploadImageAsync(photo);

             model.fotoPerfil = uploadImagem.ToString();

            var response = await _client.PostAsJsonAsync("api/Perfils/Update", model);




                return RedirectToAction("Index", "Home");
            
        }

        // GET: Perfil/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Perfil/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
