using System;
using System.Collections.Generic;

namespace WILF.BL.Menu
{
    public class GestionMenu
    {
        public List<BE.Menu> GetMenu(Int32 idperfil)
        {
            return new DA
                .Menu
                .RepositoryMenu()
                .GetMenu(idperfil);
        }
    }
}
