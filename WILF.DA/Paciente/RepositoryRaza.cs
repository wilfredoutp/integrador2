using System;
using System.Collections.Generic;
using System.Data;

namespace WILF.DA.Paciente
{
    public class RepositoryRaza
    {
        public void Save(BE.Raza raza)
        {
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Save_Raza";
                    db.AddParameter("@idraza", DbType.Int32, ParameterDirection.InputOutput, raza.IdRaza);
                    db.AddParameter("@idespecie", DbType.Int32, ParameterDirection.Input, raza.IdEspecie);
                    db.AddParameter("@descripcion", DbType.String, ParameterDirection.Input, raza.Descripcion);
                    db.AddParameter("@estado", DbType.Int32, ParameterDirection.Input, raza.Estado);

                    db.Execute();
                    if (db.GetParameter("@idraza").ToString() != "")
                    {
                        raza.IdRaza= Convert.ToInt32(db.GetParameter("@idraza").ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BE.Raza GetRaza(Int32 idraza)
        {
            BE.Raza result = new BE.Raza();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_RazaId";
                    db.AddParameter("@idraza", DbType.Int32, ParameterDirection.Input, idraza);
                    using (IDataReader dr = db.GetDataReader())
                    {
                        if (dr.Read())
                        {
                            result.IdRaza = Convert.ToInt32(dr["IdRaza"]);
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

        public List<BE.Raza> GetRaza()
        {
            List<BE.Raza> result = new List<BE.Raza>();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_Especie";
                    using (IDataReader dr = db.GetDataReader())
                    {
                        while (dr.Read())
                        {
                            var p = new BE.Raza
                            {
                                IdRaza = Convert.ToInt32(dr["IdRaza"]),
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

        public List<BE.Raza> GetRazaEspecieId(Int32 idespecie)
        {
            List<BE.Raza> result = new List<BE.Raza>();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_RazaIdEspecie";
                    db.AddParameter("@idespecie", DbType.Int32, ParameterDirection.Input, idespecie);
                    using (IDataReader dr = db.GetDataReader())
                    {
                        while (dr.Read())
                        {
                            var p = new BE.Raza
                            {
                                IdRaza = Convert.ToInt32(dr["IdRaza"]),
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
