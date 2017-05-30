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
using Newtonsoft.Json;

namespace RedeSocial.WEB.Controllers
{
    public class ImageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private HttpClient _client;

        public ImageController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:60551/");
            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }

        ImageService imageService = new ImageService();

        public async Task<ActionResult> Index()
        {

            var response = await _client.GetAsync("api/Imagems/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var JsonString = await response.Content.ReadAsStringAsync();

                var IMAGEM = JsonConvert.DeserializeObject<List<ImageViewModel>>(JsonString);


                return View(IMAGEM);

            }
            else
            {
                return View();

            }


        }

        // GET: Image  
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase photo )
        {
            var imageUrl = await imageService.UploadImageAsync(photo);

            
            var idusuario = Session["idUsuario"].ToString();
            var emailUsuario = Session["EmailUsuario"].ToString();
            var foto = imageUrl.ToString();

            var uri = "api/Imagems/Create?idUsuario=" + idusuario + "&emailUsuario=" + emailUsuario + "&foto=" + foto ;

            var response = await _client.GetAsync(uri);

            

            
            return RedirectToAction("LatestImage");
        }

        public ActionResult LatestImage()
        {
            var latestImage = string.Empty;
            if (TempData["LatestImage"] != null)
            {
                ViewBag.LatestImage = Convert.ToString(TempData["LatestImage"]);
            }

            return View();
        }
    }
}