using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WILF.BE
{
    public class Paciente : Base
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 IdPaciente { get; set; }
        [Key, Column(Order = 1)]
        public Int32 IdPersona { get; set; }
        [Key, Column(Order = 2)]
        public Int32 IdRaza { get; set; }
        public string Nombre { get; set; }
        public Nullable<DateTime> FecNacimiento { get; set; }
        public Nullable<int> Edad { get; set; }
        public string RutaImagen { get; set; }
        public Nullable<Int32> IdEspecie { get; set; }
    }
}
