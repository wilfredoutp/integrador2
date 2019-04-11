using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WILF.BE
{
    public class Raza : Base
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 IdRaza { get; set; }
        [Key, Column(Order = 1)]
        public Int32 IdEspecie { get; set; }
        public string Descripcion { get; set; }
    }
}
