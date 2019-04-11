using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WILF.BE
{
    public class Menu : Base
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 IdMenu { get; set; }
        public Nullable<Int32> IdPadre { get; set; }
        public string Descripcion { get; set; }
        public string Ruta { get; set; }
        public string RutaImagen { get; set; }
        public string Clase { get; set; }
    }
}
