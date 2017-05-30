using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace RedeSocial.Data
{
    [Table("Imagem")]
    public class Imagem
    {
        [Key]
        public int id { get; set; }


        public string idUsuario { get; set; }


        public string emailUsuario { get; set; }

        public string foto { get; set; }

    }
}
