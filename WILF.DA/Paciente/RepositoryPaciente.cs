using System;
using System.Collections.Generic;
using System.Data;

namespace WILF.DA.Paciente
{
    public class RepositoryPaciente
    {
        public void Save(BE.Paciente paciente)
        {
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Save_Paciente";
                    db.AddParameter("@idpaciente", DbType.Int32, ParameterDirection.InputOutput, paciente.IdPaciente);
                    db.AddParameter("@idpersona", DbType.Int32, ParameterDirection.Input, paciente.IdPersona);
                    db.AddParameter("@idraza", DbType.Int32, ParameterDirection.Input, paciente.IdRaza);
                    db.AddParameter("@nombre", DbType.String, ParameterDirection.Input, paciente.Nombre);
                    db.AddParameter("@fechanaci", DbType.DateTime, ParameterDirection.Input, paciente.FecNacimiento);
                    db.AddParameter("@edad", DbType.Int32, ParameterDirection.Input, paciente.Edad);
                    db.AddParameter("@rutaimagen", DbType.String, ParameterDirection.Input, paciente.RutaImagen);
                    db.AddParameter("@estado", DbType.Int32, ParameterDirection.Input, paciente.Estado);

                    db.Execute();
                    if (db.GetParameter("@idpaciente").ToString() != "")
                    {
                        paciente.IdPaciente = Convert.ToInt32(db.GetParameter("@idpaciente").ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BE.Paciente GetPaciente(Int32 idpaciente)
        {
            BE.Paciente result = new BE.Paciente();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_PacienteId";
                    db.AddParameter("@idpaciente", DbType.Int32, ParameterDirection.Input, idpaciente);
                    using (IDataReader dr = db.GetDataReader())
                    {
                        if (dr.Read())
                        {
                            result.IdPaciente = Convert.ToInt32(dr["IdPaciente"]);
                            result.IdPersona = Convert.ToInt32(dr["IdPersona"]);
                            result.IdRaza = Convert.ToInt32(dr["IdRaza"]);
                            result.Nombre = Convert.ToString(dr["Nombre"]);
                            result.FecNacimiento = Convert.ToDateTime(dr["FecNacimiento"]);
                            result.Edad = Convert.ToInt32(dr["Edad"]);
                            result.RutaImagen = Convert.ToString(dr["RutaImagen"]);
                            result.Estado = Convert.ToInt32(dr["Estado"]);
                            result.Fecha = Convert.ToDateTime(dr["Fecha"]);
                            if (dr["FechaMod"].ToString() != "") result.FechaMod = Convert.ToDateTime(dr["FechaMod"]);
                            if (dr["IdEspecie"].ToString() != "") result.IdEspecie = Convert.ToInt32(dr["IdEspecie"]);
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

        public List<BE.Paciente> GetPacientexPersonaId(Int32 idpersona)
        {
            List<BE.Paciente> result = new List<BE.Paciente>();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_PacientexPersonaId";
                    db.AddParameter("@idpersona", DbType.Int32, ParameterDirection.Input, idpersona);
                    using (IDataReader dr = db.GetDataReader())
                    {
                        while (dr.Read())
                        {
                            var p = new BE.Paciente
                            {
                                IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                IdRaza = Convert.ToInt32(dr["IdRaza"]),
                                Nombre = Convert.ToString(dr["Nombre"]),
                                FecNacimiento = Convert.ToDateTime(dr["FecNacimiento"]),
                                Edad = Convert.ToInt32(dr["Edad"]),
                                RutaImagen = Convert.ToString(dr["RutaImagen"]),
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
