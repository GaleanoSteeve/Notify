using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DatMenu
    {
        public DataTable ListarPermisos(int IdPerfil)
        {
            DataTable dtDatos = new DataTable();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpPerfiles";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LM";
                sqlCommand.Parameters.Add("@IdPerfil", SqlDbType.Int).Value = IdPerfil;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dtDatos);
                return dtDatos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlCommand.Connection.State == ConnectionState.Open)
                {
                    sqlCommand.Connection.Close();
                }
                sqlCommand = null;
            }
        }
    }
}
