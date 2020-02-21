using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programas.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Programas.DataAccess
{
    public class Conexion : DbContext,IDisposable
    {
        private SqlConnection conn;
        //private ILogger logger = LoggerFactory.GetLogger(typeof(Conexion));
        //private static readonly ILog _logger = LogManager.GetLogger(typeof(Conexion));

        public Conexion(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
        }

        //public Conexion(DbContextOptions<Conexion> options)
        //  : base(options)
        //{
        //}



        public SqlDataReader EjecutarSP(string strSP)
        {
            return EjecutarSP(strSP, null);
        }

        public SqlDataReader EjecutarSqlReader(string queryString)
        {
            using (SqlCommand cmd = new SqlCommand(queryString, conn))
            {
                return cmd.ExecuteReader();
            }
        }

        public int EjecutarSqlABM(string query)
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public int EjecutarSqlABM(string query, SqlParameter[] parameters)
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        public object EjecutarSqlConId(string query)
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                return cmd.ExecuteScalar();
            }
        }

        public SqlDataReader EjecutarSP(string strSP, Diccionario Params)
        {
            using (SqlCommand cmd = new SqlCommand(strSP, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (this.trans != null)
                    cmd.Transaction = this.trans;
                SqlCommandBuilder.DeriveParameters(cmd);
                string strPar = "";
                foreach (SqlParameter par in cmd.Parameters)
                {
                    if (Params != null)
                        if (Params.ContainsKey(par.ParameterName.Replace("@", "")))
                        {
                            par.Value = Params[par.ParameterName.Replace("@", "")];
                            if (par.Value == null)
                                par.Value = DBNull.Value;
                        }
                        else
                        {
                            if (!par.IsNullable)
                            {
                                if ((par.DbType == DbType.Int16) ||
                                    (par.DbType == DbType.Int32) ||
                                    (par.DbType == DbType.Int64))
                                    par.Value = 0;
                                else if (par.DbType == DbType.Boolean)
                                    par.Value = false;
                                else if (par.DbType == DbType.DateTime)
                                    par.Value = DateTime.Now;
                                else

                                    par.Value = "";
                            }
                        }
                    if (par.Value == null)
                        strPar = strPar + ",null";
                    else if (par.DbType == DbType.AnsiString)
                        strPar = strPar + ",'" + par.Value.ToString() + "'";
                    else if (par.DbType == DbType.String)
                        strPar = strPar + ",'" + par.Value.ToString() + "'";
                    else if (par.DbType == DbType.DateTime)
                        strPar = strPar + ",'" + par.Value.ToString() + "'";
                    else
                        strPar = strPar + "," + par.Value.ToString();
                }
                //_logger.Info("Ejecuta el sp " + strSP + " con los parametros " + strPar);
        //        logger.DebugFormat("Sp: {0} {1}", cmd.CommandText, strPar.Substring(1));
                SqlDataReader dr = cmd.ExecuteReader();

                if (Params != null)
                    foreach (SqlParameter par in cmd.Parameters)
                    {
                        if ((par.Direction == ParameterDirection.Output) || (par.Direction == ParameterDirection.InputOutput))
                        {
                            Params[par.ParameterName.Replace("@", "")] = par.Value;
                        }
                    }

                return dr;
            }
        }

        internal void Close()
        {
            conn.Close();
        }

        public object ObtenerValor(string queryString, string Campo)
        {
            object valor = null;
            using (SqlCommand cmd = new SqlCommand(queryString, conn))
            {
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        valor = reader[Campo];
                    }
                }

                reader.Close();
            }

            return valor;
        }

        public void Dispose()
        {
            conn.Dispose();
        }

        public SqlTransaction trans
        {
            get;
            set;
        }

        public void BeginTransaction()
        {
            trans = conn.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            trans.Rollback();
            this.trans = null;
        }

        public void CommitTransaction()
        {
            trans.Commit();
            this.trans = null;
        }
    }
}