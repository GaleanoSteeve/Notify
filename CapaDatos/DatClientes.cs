using System;
using CapaObjetos;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DatClientes
    {
        public bool Eliminar(ObjClientes oCliente)
        {
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpAdminClientes";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "D";
                sqlCommand.Parameters.Add("@Documento", SqlDbType.BigInt).Value = oCliente.Documento;
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
        public bool Almacenar(ObjClientes oCliente)
        {
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpAdminClientes";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "C";
                sqlCommand.Parameters.Add("@IdCliente", SqlDbType.Int).Value = oCliente.IdCliente;
                sqlCommand.Parameters.Add("@TipoDocumento", SqlDbType.Int).Value = oCliente.TipoDocumento;
                sqlCommand.Parameters.Add("@Documento", SqlDbType.BigInt).Value = oCliente.Documento;
                sqlCommand.Parameters.Add("@Nombres", SqlDbType.VarChar, 100).Value = oCliente.Nombres;
                sqlCommand.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100).Value = oCliente.Apellidos;
                sqlCommand.Parameters.Add("@IdPais", SqlDbType.Int).Value = oCliente.IdPais;
                sqlCommand.Parameters.Add("@Pais", SqlDbType.VarChar, 100).Value = oCliente.Pais;
                sqlCommand.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = oCliente.IdDepartamento;
                sqlCommand.Parameters.Add("@Departamento", SqlDbType.VarChar, 100).Value = oCliente.Departamento;
                sqlCommand.Parameters.Add("@IdMunicipio", SqlDbType.Int).Value = oCliente.IdMunicipio;
                sqlCommand.Parameters.Add("@Municipio", SqlDbType.VarChar, 100).Value = oCliente.Municipio;
                sqlCommand.Parameters.Add("@IdCorregimiento", SqlDbType.Int).Value = oCliente.IdCorregimiento;
                sqlCommand.Parameters.Add("@Corregimiento", SqlDbType.VarChar, 100).Value = oCliente.Corregimiento;
                sqlCommand.Parameters.Add("@IdVereda", SqlDbType.Int).Value = oCliente.IdVereda;
                sqlCommand.Parameters.Add("@Vereda", SqlDbType.VarChar, 100).Value = oCliente.Vereda;
                sqlCommand.Parameters.Add("@Barrio", SqlDbType.VarChar, 000).Value = oCliente.Barrio;
                sqlCommand.Parameters.Add("@Direccion", SqlDbType.VarChar, 200).Value = oCliente.Direccion;
                sqlCommand.Parameters.Add("@WhatsApp", SqlDbType.BigInt).Value = oCliente.WhatsApp;
                sqlCommand.Parameters.Add("@Telefono1", SqlDbType.BigInt).Value = oCliente.Telefono1;
                sqlCommand.Parameters.Add("@Telefono2", SqlDbType.BigInt).Value = oCliente.Telefono2;
                sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = oCliente.Email;
                sqlCommand.Parameters.Add("@Estado", SqlDbType.Bit).Value = oCliente.Estado;
                sqlCommand.Parameters.Add("@UsuarioCreacion", SqlDbType.VarChar, 50).Value = oCliente.UsuarioCreacion;
                sqlCommand.Parameters.Add("@UsuarioModificacion", SqlDbType.VarChar, 50).Value = oCliente.UsuarioModificacion;
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
        public DataTable ListarClientes()
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
                sqlCommand.CommandText = "stpAdminClientes";
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
        public DataTable ListarMaximoIdCliente()
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
                sqlCommand.CommandText = "stpAdminClientes";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "MAX";
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
        public DataTable ListarComboTipoDocumentos()
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
                sqlCommand.CommandText = "stpAdminClientes";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LCTP";
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
        public DataTable ListarCliente(long Documento)
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
                sqlCommand.CommandText = "stpAdminClientes";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LC";
                sqlCommand.Parameters.Add("@Documento", SqlDbType.BigInt).Value = Documento;
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
        public DataTable ExisteDocumento(ObjClientes oCliente)
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
                sqlCommand.CommandText = "stpAdminClientes";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "ED";
                sqlCommand.Parameters.Add("@Documento", SqlDbType.BigInt).Value = oCliente.Documento;
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
        public DataTable ListarClientesParametros(string Parametro)
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
                sqlCommand.CommandText = "stpAdminClientes";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LCP";
                sqlCommand.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = Parametro;
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