using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WILF.BE
{
    public class PerfilMenu : Base
    {
        [Key, Column(Order = 0)]
        public Int32 IdPerfil { get; set; }
        [Key, Column(Order = 1)]
        public Int32 IdMenu { get; set; }
    }
}
