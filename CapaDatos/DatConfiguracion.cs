using System;
using System.Data;
using CapaObjetos;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DatConfiguracion
    {
        //listar
        public DataTable ListarConfiguracion()
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
                sqlCommand.CommandText = "stpConfiguracion";
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

        public string Guardar(ObjConfiguracion oConfiguracion)
        {
            string Resultado = "";
            SqlDataReader sqlDataReader;
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpConfiguracion";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "C";
                sqlCommand.Parameters.Add("@Documento", SqlDbType.BigInt).Value = oConfiguracion.Documento;
                sqlCommand.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 100).Value = oConfiguracion.RazonSocial;
                sqlCommand.Parameters.Add("@NombreComercial", SqlDbType.VarChar, 100).Value = oConfiguracion.NombreComercial;
                sqlCommand.Parameters.Add("@IdDepartamento", SqlDbType.VarChar, 10).Value = oConfiguracion.IdDepartamento;
                sqlCommand.Parameters.Add("@Departamento", SqlDbType.VarChar, 100).Value = oConfiguracion.Departamento;
                sqlCommand.Parameters.Add("@IdCiudad", SqlDbType.VarChar, 10).Value = oConfiguracion.IdCiudad;
                sqlCommand.Parameters.Add("@Ciudad", SqlDbType.VarChar, 100).Value = oConfiguracion.Ciudad;
                sqlCommand.Parameters.Add("@Direccion", SqlDbType.VarChar, 200).Value = oConfiguracion.Direccion;
                sqlCommand.Parameters.Add("@Telefono", SqlDbType.BigInt).Value = oConfiguracion.Telefono;
                sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = oConfiguracion.Email;
                sqlCommand.Parameters.Add("@UsuarioCreacion", SqlDbType.VarChar, 50).Value = oConfiguracion.UsuarioCreacion;
                sqlCommand.Parameters.Add("@EquipoCreacion", SqlDbType.VarChar, 50).Value = oConfiguracion.EquipoCreacion;
                sqlCommand.Parameters.Add("@UsuarioModificacion", SqlDbType.VarChar, 50).Value = oConfiguracion.UsuarioModificacion;
                sqlCommand.Parameters.Add("@EquipoModificacion", SqlDbType.VarChar, 50).Value = oConfiguracion.EquipoModificacion;
                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Resultado = sqlDataReader.GetString(0);
                }
                return Resultado;
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
