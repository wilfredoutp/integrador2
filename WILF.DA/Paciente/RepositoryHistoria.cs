using System;
using System.Collections.Generic;
using System.Data;

namespace WILF.DA.Paciente
{
    public class RepositoryHistoria
    {
        public void Save(BE.Historial historial)
        {
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Save_Historial";
                    db.AddParameter("@idhistorial", DbType.Int32, ParameterDirection.InputOutput, historial.IdHistoria);
                    db.AddParameter("@idpaciente", DbType.Int32, ParameterDirection.Input, historial.IdPaciente);
                    db.AddParameter("@tratamiento", DbType.String, ParameterDirection.Input, historial.Tratamiento);
                    db.AddParameter("@detalle", DbType.String, ParameterDirection.Input, historial.Detalle);
                    db.AddParameter("@estado", DbType.Int32, ParameterDirection.Input, historial.Estado);

                    db.Execute();
                    if (db.GetParameter("@idhistorial").ToString() != "")
                    {
                        historial.IdHistoria = Convert.ToInt32(db.GetParameter("@idhistorial").ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BE.Historial GetHistorialId(Int32 idhistorial)
        {
            BE.Historial result = new BE.Historial();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_HistorialId";
                    db.AddParameter("@idhistorial", DbType.Int32, ParameterDirection.Input, idhistorial);
                    using (IDataReader dr = db.GetDataReader())
                    {
                        if (dr.Read())
                        {
                            result.IdHistoria = Convert.ToInt32(dr["IdHistoria"]);
                            result.IdPaciente = Convert.ToInt32(dr["IdPaciente"]);
                            result.Tratamiento = Convert.ToString(dr["Tratamiento"]);
                            result.Detalle = Convert.ToString(dr["Detalle"]);
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

        public List<BE.Historial> GetHistorialPaciente(Int32 idpaciente)
        {
            List<BE.Historial> result = new List<BE.Historial>();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_HistorialPacienteId";
                    db.AddParameter("@idpaciente", DbType.Int32, ParameterDirection.Input, idpaciente);
                    using (IDataReader dr = db.GetDataReader())
                    {
                        while (dr.Read())
                        {
                            var p = new BE.Historial
                            {
                                IdHistoria = Convert.ToInt32(dr["IdHistoria"]),
                                IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                Tratamiento = Convert.ToString(dr["Tratamiento"]),
                                Detalle = Convert.ToString(dr["Detalle"]),
                                Estado = Convert.ToInt32(dr["Estado"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
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
