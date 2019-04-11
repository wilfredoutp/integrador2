using System;
using System.Collections.Generic;
using System.Data;
namespace WILF.DA.Paciente
{
    public class RepositoryEspecie
    {
        public void Save(BE.Especie especie)
        {
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Save_Especie";
                    db.AddParameter("@idespecie", DbType.Int32, ParameterDirection.InputOutput, especie.IdEspecie);
                    db.AddParameter("@descripcion", DbType.String, ParameterDirection.Input, especie.Descripcion);
                    db.AddParameter("@estado", DbType.Int32, ParameterDirection.Input, especie.Estado);

                    db.Execute();
                    if (db.GetParameter("@idespecie").ToString() != "")
                    {
                        especie.IdEspecie= Convert.ToInt32(db.GetParameter("@idespecie").ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BE.Especie GetEspecie(Int32 idespecie)
        {
            BE.Especie result = new BE.Especie();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_EspecieId";
                    db.AddParameter("@idespecie", DbType.Int32, ParameterDirection.Input, idespecie);
                    using (IDataReader dr = db.GetDataReader())
                    {
                        if (dr.Read())
                        {
                            result.IdEspecie = Convert.ToInt32(dr["IdEspecie"]);
                            result.Descripcion = Convert.ToString(dr["Descripcion"]);
                            result.Estado = Convert.ToInt32(dr["Estado"]);
                            result.Fecha = Convert.ToDateTime(dr["Fecha"]);
                            if (dr["FechaMod"].ToString() != "") result.FechaMod = Convert.ToDateTime(dr["FechaMod"]);
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

        public List<BE.Especie> GetEspecie()
        {
            List<BE.Especie> result = new List<BE.Especie>();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_Especie";
                    using (IDataReader dr = db.GetDataReader())
                    {
                        while (dr.Read())
                        {
                            var p = new BE.Especie
                            {
                                IdEspecie = Convert.ToInt32(dr["IdEspecie"]),
                                Descripcion = Convert.ToString(dr["Descripcion"]),
                                Estado = Convert.ToInt32(dr["Estado"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"])
                            };
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
