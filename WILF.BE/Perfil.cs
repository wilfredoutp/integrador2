using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WILF.BE
{
    public class Perfil : Base
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 IdPerfil { get; set; }
        public string Descripcio { get; set; }
    }
}
