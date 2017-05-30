using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedeSocial.WEB.Models
{
    public class ImageViewModel
    {
        [Key]
        public int id { get; set; }


        public string idUsuario { get; set; }


        public string emailUsuario { get; set; }

        public string foto { get; set; }
    }
}