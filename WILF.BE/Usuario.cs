using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WILF.BE
{
    public class Usuario : Base
    {
        [Key, Column(Order = 0)]
        public Int32 IdPersona { get; set; }
        [Key, Column(Order = 1)]
        public Int32 IdPerfil { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string RutaImagen { get; set; }
        public string ImagenMascota { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
