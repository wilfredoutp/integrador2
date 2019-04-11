using System;
using System.Collections.Generic;
using System.Data;

namespace WILF.DA.Persona
{
    public class RepositoryPersona
    {
        public void Save(BE.Persona persona)
        {
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Save_Persona";
                    db.AddParameter("@idpersona", DbType.Int32, ParameterDirection.InputOutput, persona.IdPersona);
                    db.AddParameter("@dni", DbType.String, ParameterDirection.Input, persona.Dni);
                    db.AddParameter("@nombre", DbType.String, ParameterDirection.Input, persona.Nombre);
                    db.AddParameter("@appaterno", DbType.String, ParameterDirection.Input, persona.ApPaterno);
                    db.AddParameter("@apmaterno", DbType.String, ParameterDirection.Input, persona.ApMaterno);
                    db.AddParameter("@direcion", DbType.String, ParameterDirection.Input, persona.Direccion);
                    db.AddParameter("@telefono", DbType.String, ParameterDirection.Input, persona.Telefono);
                    db.AddParameter("@email", DbType.String, ParameterDirection.Input, persona.Email);
                    db.AddParameter("@rutaimagen", DbType.String, ParameterDirection.Input, persona.RutaImagen);
                    db.AddParameter("@estado", DbType.Int32, ParameterDirection.Input, persona.Estado);

                    db.Execute();
                    if (db.GetParameter("@idpersona").ToString() != "")
                    {
                        persona.IdPersona = Convert.ToInt32(db.GetParameter("@idpersona").ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BE.Persona GetCliente(Int32 idpersona)
        {
            BE.Persona result = new BE.Persona();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_ClienteId";
                    db.AddParameter("@idpersona", DbType.Int32, ParameterDirection.Input, idpersona);
                    using (IDataReader dr = db.GetDataReader())
                    {
                        if (dr.Read())
                        {
                            result.IdPersona = Convert.ToInt32(dr["IdPersona"]);
                            result.Dni = Convert.ToString(dr["Dni"]);
                            result.Nombre = Convert.ToString(dr["Nomber"]);
                            result.ApPaterno = Convert.ToString(dr["ApPaterno"]);
                            result.ApMaterno = Convert.ToString(dr["ApMaterno"]);
                            result.Direccion = Convert.ToString(dr["Direccion"]);
                            result.Telefono = Convert.ToString(dr["Telefono"]);
                            result.Email = Convert.ToString(dr["Email"]);
                            result.RutaImagen = Convert.ToString(dr["RutaImagen"]);
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

        public List<BE.Persona> GetCliente()
        {
            List<BE.Persona> result = new List<BE.Persona>();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_Persona";
                    using (IDataReader dr = db.GetDataReader())
                    {
                        while (dr.Read())
                        {
                            var p = new BE.Persona
                            {
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                Dni = Convert.ToString(dr["Dni"]),
                                Nombre = Convert.ToString(dr["Nomber"]),
                                ApPaterno = Convert.ToString(dr["ApPaterno"]),
                                ApMaterno = Convert.ToString(dr["ApMaterno"]),
                                Direccion = Convert.ToString(dr["Direccion"]),
                                Telefono = Convert.ToString(dr["Telefono"]),
                                Email = Convert.ToString(dr["Email"]),
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

        public List<BE.Persona> GetCliente(string dni, string nombre)
        {
            List<BE.Persona> result = new List<BE.Persona>();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Get_Cliente";
                    db.AddParameter("@dni", DbType.String, ParameterDirection.Input, dni);
                    db.AddParameter("@nombre", DbType.String, ParameterDirection.Input, nombre);
                    using (IDataReader dr = db.GetDataReader())
                    {
                        while (dr.Read())
                        {
                            var p = new BE.Persona
                            {
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                Dni = Convert.ToString(dr["Dni"]),
                                Nombre = Convert.ToString(dr["Nomber"]),
                                Direccion = Convert.ToString(dr["Direccion"]),
                                Telefono = Convert.ToString(dr["Telefono"]),
                                Email = Convert.ToString(dr["Email"]),
                                Estado = Convert.ToInt32(dr["Estado"])
                            };
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
