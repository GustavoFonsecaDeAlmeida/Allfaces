using Newtonsoft.Json;
using RedeSocial.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Microsoft.AspNet.Identity.Owin;

namespace RedeSocial.WEB.Controllers
{
    public class HomeController : Controller
    {

        private HttpClient _client;
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:60551/");
            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);


        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }





        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<ActionResult> Principal()
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

        [HttpGet]
        [AllowAnonymous]

        public async Task<ActionResult> ImagePrincipal()
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




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}