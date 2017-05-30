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
    public class PostagemController : Controller
    {

        private HttpClient _client;


        public PostagemController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:60551/");
            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }

        ImageService imageService = new ImageService();


        [HttpGet]
        [AllowAnonymous]

        public async Task<ActionResult> Index()
        {

            var response = await _client.GetAsync("api/postagems/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var JsonString = await response.Content.ReadAsStringAsync();

                var POSTAGEM = JsonConvert.DeserializeObject<List<PostagemViewModel>>(JsonString);


                return View(POSTAGEM);

            }
            else
            {
                return View();

            }


        }

        // GET: Postagem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Postagem/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Postagem/Create
        //
        // POST: /Postagem/Register
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostagemViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.idUsuario = Session["idUsuario"].ToString();
                model.emailUsuario = Session["EmailUsuario"].ToString();

                var response = await _client.PostAsJsonAsync("api/Postagems/Create", model);

                return RedirectToAction("Principal", "Home");

            }
            else
            {
                return View();
            }

        }

        [Authorize]
        public ActionResult PostarImagem()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostarImagem(PostagemViewModel model, HttpPostedFileBase photo)
        {


            model.idUsuario = Session["idUsuario"].ToString();
            model.emailUsuario = Session["EmailUsuario"].ToString();

            if (photo == null )
            {
                model.imagem = "Sem Imagem";

            }
            else
            {
                var uploadImagem = await imageService.UploadImageAsync(photo);

                model.imagem = uploadImagem.ToString();

            }
   
            var response = await _client.PostAsJsonAsync("api/Postagems/Create", model);

            return RedirectToAction("Principal", "Home");







        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase photo)
        {
            var imageUrl = await imageService.UploadImageAsync(photo);


            var idusuario = Session["idUsuario"].ToString();
            var emailUsuario = Session["EmailUsuario"].ToString();
            var foto = imageUrl.ToString();

            var uri = "api/Imagems/Create?idUsuario=" + idusuario + "&emailUsuario=" + emailUsuario + "&foto=" + foto;

            var response = await _client.GetAsync(uri);




            return RedirectToAction("LatestImage");
        }

        // GET: Postagem/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Postagem/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// GET: Postagem/Delete/5
        //public ActionResult Delete()
        //{

        //    return View();
        //}


        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {

            var uri = "api/Postagems/Deletar?id=" + id;
            var response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Principal", "Home");
            }


            return View();






        }
    }
}
