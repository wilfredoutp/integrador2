using System;
using System.Data;
using System.Data.Common;
namespace WILF.DA
{
    public class Database : IDisposable
    {
        private string _ConnectionString = string.Empty;
        private string _Esquema = string.Empty;

        [NonSerialized]
        DbCommand command;
        [NonSerialized]
        DbConnection connection;
        [NonSerialized]
        DbProviderFactory factory;

        #region Constructor
        public Database(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
            try
            {
                this.factory = DbProviderFactories.GetFactory(DatabaseHelper.GetDbProvider(this._ConnectionString));
                if (this.command == null)
                {
                    this.command = this.factory.CreateCommand();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error:" + ex.Message);
            }
        }

        ~Database()
        {
            this.Dispose();
        }

        #endregion

        public void AddParameter(string parameterName, DbType parameterType, ParameterDirection parameterDirection)
        {
            if (this.command != null)
            {
                DbParameter param = this.factory.CreateParameter();
                param.ParameterName = parameterName;
                param.DbType = parameterType;
                param.Direction = parameterDirection;
                this.command.Parameters.Add(param);
            }
        }

        public void AddParameter(string parameterName, DbType parameterType, int parameterSize)
        {
            if (this.command != null)
            {
                DbParameter param = this.factory.CreateParameter();
                param.ParameterName = parameterName;
                param.DbType = parameterType;
                param.Direction = ParameterDirection.Output;
                param.Size = parameterSize;
                this.command.Parameters.Add(param);
            }
        }

        public void AddParameter(string parameterName, DbType parameterType, ParameterDirection parameterDirection, object parameterValue)
        {
            if (this.command != null)
            {
                DbParameter param = this.factory.CreateParameter();
                param.ParameterName = parameterName;
                param.DbType = parameterType;
                param.Direction = parameterDirection;
                if (parameterValue != null)
                {
                    if (parameterValue.ToString() == "")
                        param.Value = DBNull.Value;
                    else param.Value = parameterValue;
                }
                else param.Value = DBNull.Value;

                this.command.Parameters.Add(param);
            }
        }
        public void AddParameter(string parameterName, ParameterDirection parameterDirection, object parameterValue)
        {
            if (this.command != null)
            {
                DbParameter param = this.factory.CreateParameter();
                param.ParameterName = parameterName;
                param.Direction = parameterDirection;
                if (parameterValue != null)
                {
                    if (parameterValue.ToString() == "")
                        param.Value = DBNull.Value;
                    else param.Value = parameterValue;
                }
                else param.Value = DBNull.Value;

                this.command.Parameters.Add(param);
            }
        }
        public void AddParameter(string parameterName, DbType parameterType, ParameterDirection parameterDirection, int Size, object parameterValue)
        {
            if (this.command != null)
            {
                DbParameter param = this.factory.CreateParameter();
                param.ParameterName = parameterName;
                param.DbType = parameterType;
                param.Direction = parameterDirection;
                param.Size = Size;
                if (parameterValue != null)
                {
                    if (parameterValue.ToString() == "")
                        param.Value = DBNull.Value;
                    else param.Value = parameterValue;
                }
                else param.Value = DBNull.Value;

                this.command.Parameters.Add(param);
            }
        }
        public void Close()
        {
            this.connection.Close();
        }

        public void Dispose()
        {
            this.connection = null;
            this.command = null;
            this.factory = null;
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        public int Execute()
        {
            return this.command.ExecuteNonQuery();
        }

        public int Execute(DbConnection con, string spName)
        {
            if (this.command == null)
            {
                this.command = this.factory.CreateCommand();
            }
            this.command.Connection = con;
            this.command.CommandType = CommandType.StoredProcedure;
            this.command.CommandText = spName;
            if (this.command.Transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((this.command.Transaction != null) && (this.command.Transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            return this.command.ExecuteNonQuery();
        }

        public int Execute(DbConnection con, string spName, DbParameter[] parameters)
        {
            if (this.command == null)
            {
                this.command = this.factory.CreateCommand();
            }
            this.command.Connection = con;
            this.command.CommandType = CommandType.StoredProcedure;
            this.command.CommandText = spName;
            this.command.Parameters.AddRange(parameters);
            if (this.command.Transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((this.command.Transaction != null) && (this.command.Transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            return this.command.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            return this.command.ExecuteScalar();
        }

        public DbConnection GetConnection()
        {
            connection.ConnectionString = DatabaseHelper.GetDbConnectionString(this._ConnectionString);
            connection.Open();
            return connection;
        }

        public DbDataReader GetDataReader()
        {
            return this.command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();

            dt.Load(this.command.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SequentialAccess));

            return dt;
        }

        public object GetParameter(int index)
        {
            if (this.command != null)
            {
                try
                {
                    return this.command.Parameters[index].Value;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public object GetParameter(string name)
        {
            if (this.command != null)
            {
                try
                {
                    return this.command.Parameters[name].Value;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public void Open()
        {
            this.connection = this.factory.CreateConnection();
            this.connection.ConnectionString = DatabaseHelper.GetDbConnectionString(this._ConnectionString);
            try
            {
                this.connection.Open();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string CommandText
        {
            set
            {
                if (this.command == null)
                {
                    this.command = this.factory.CreateCommand();
                }
                if (this.connection == null)
                {
                    this.Open();
                }
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.Text;
                this.command.CommandText = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this._ConnectionString;
            }
        }

        public string Esquema
        {
            get
            {
                return this._Esquema;
            }
        }

        public string ProcedureName
        {
            set
            {
                if (this.command == null)
                {
                    this.command = this.factory.CreateCommand();
                }
                if (this.connection == null)
                {
                    this.Open();
                }
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = value;
            }
        }
    }
}
