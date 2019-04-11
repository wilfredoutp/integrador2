using System;
using System.Data;
namespace WILF.DA.Usuario
{
    public class RepositoryUsuario
    {
        public bool Valid(string user,string password)
        {
            bool result = false;
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_User_Valid";
                    db.AddParameter("@User", DbType.String, ParameterDirection.Input, user);
                    db.AddParameter("@Password", DbType.String, ParameterDirection.Input, password);

                    var valid = db.ExecuteScalar();
                    if (valid != null && valid.ToString() != "")
                    {
                        result = Convert.ToInt32(valid) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public BE.Usuario GetUser(string user)
        {
            BE.Usuario result = new BE.Usuario();
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_User_Get";
                    db.AddParameter("@User", DbType.String, ParameterDirection.Input, user);
                    using (IDataReader dr = db.GetDataReader())
                    {
                        if (dr.Read())
                        {
                            result.IdPerfil = Convert.ToInt32(dr["IdPerfil"]);
                            result.IdPersona = Convert.ToInt32(dr["IdPersona"]);
                            result.User = Convert.ToString(dr["User"]);
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

        public void Save(BE.Usuario usuario)
        {
            try
            {
                using (Database db = new Database(DatabaseHelper.ConexionData))
                {
                    db.ProcedureName = "P_Save_Usuario";
                    db.AddParameter("@idpersona", DbType.Int32, ParameterDirection.Input, usuario.IdPersona);
                    db.AddParameter("@idperfiel", DbType.Int32, ParameterDirection.Input, usuario.IdPerfil);
                    db.AddParameter("@user", DbType.String, ParameterDirection.Input, usuario.User);
                    db.AddParameter("@password", DbType.String, ParameterDirection.Input, usuario.Password);
                    db.AddParameter("@estado", DbType.Int32, ParameterDirection.Input, usuario.Estado);
                    db.Execute();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
