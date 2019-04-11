using System;
using System.Collections.Generic;
using System.Data;

namespace WILF.DA.Menu
{
    public class RepositoryMenu
    {
        public List<BE.Menu> GetMenu(Int32 idperfil)
        {
            List<BE.Menu> result = new List<BE.Menu>();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_Menu";
                    db.AddParameter("@idperfil", DbType.Int32, ParameterDirection.Input, idperfil);
                    using (IDataReader dr = db.GetDataReader())
                    {
                        while (dr.Read())
                        {
                            var p = new BE.Menu
                            {
                                IdMenu = Convert.ToInt32(dr["IdMenu"]),
                                Descripcion = Convert.ToString(dr["Descripcion"]),
                                Ruta = Convert.ToString(dr["Ruta"]),
                                RutaImagen = Convert.ToString(dr["RutaImagen"]),
                                Clase = Convert.ToString(dr["Clase"]),
                                Estado = Convert.ToInt32(dr["Estado"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"])
                            };
                            if (dr["IdPadre"].ToString() != "") p.IdPadre = Convert.ToInt32(dr["IdPadre"]);
                            if (dr["FechaMod"].ToString() != "") p.FechaMod = Convert.ToDateTime(dr["FechaMod"]);
                            result.Add(p);
                            p = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
