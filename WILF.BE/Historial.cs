using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WILF.BE
{
    public class Historial : Base
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 IdHistoria { get; set; }
        [Key, Column(Order = 1)]
        public Int32 IdPaciente { get; set; }
        public string Tratamiento { get; set; }
        public string Detalle { get; set; }
    }
}
