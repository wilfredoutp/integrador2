using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WILF.BE
{
    public class Especie : Base
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 IdEspecie { get; set; }
        public string Descripcion { get; set; }
    }
}
