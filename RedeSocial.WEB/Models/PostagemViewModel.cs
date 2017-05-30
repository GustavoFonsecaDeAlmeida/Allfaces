using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedeSocial.WEB.Models
{
    public class PostagemViewModel
    {
        [Key]
        public int id { get; set; }


        public string idUsuario { get; set; }


        public string emailUsuario { get; set; }

        [Required]
        [MaxLength(140)]
        [MinLength(10)]
        public string Conteudo { get; set; }


        public string imagem { get; set; }

    }
}