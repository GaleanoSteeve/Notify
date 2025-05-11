using System;
using System.Data;
using CapaObjetos;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DatNotificaciones
    {
        public bool Enviar(ObjNotificaciones oNotificaciones)
        {
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpNotificaciones";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "C";
                sqlCommand.Parameters.Add("@IdClienteLote", SqlDbType.Int).Value = oNotificaciones.IdClienteLote;
                sqlCommand.Parameters.Add("@Documento", SqlDbType.BigInt).Value = oNotificaciones.Documento;
                sqlCommand.Parameters.Add("@Cliente", SqlDbType.VarChar, 100).Value = oNotificaciones.Cliente;
                sqlCommand.Parameters.Add("@FechaPagoCuota", SqlDbType.Date).Value = oNotificaciones.FechaPagoCuota;
                sqlCommand.Parameters.Add("@MedioNotificacion", SqlDbType.VarChar, 20).Value = oNotificaciones.MedioNotificacion;
                sqlCommand.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Value = oNotificaciones.Mensaje;
                sqlCommand.Parameters.Add("@UsuarioCreacion", SqlDbType.VarChar, 50).Value = oNotificaciones.UsuarioCreacion;
                sqlCommand.ExecuteReader();
                return true;
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

        //Listar
        public DataTable ListarRegistros()
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
                sqlCommand.CommandText = "stpNotificaciones";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "L";
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