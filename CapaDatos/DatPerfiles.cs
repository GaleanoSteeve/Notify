using System;
using System.Data;
using CapaObjetos;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DatPerfiles
    {
        public bool Guardar(ObjPerfiles oPerfil)
        {
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpPerfiles";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "C";
                sqlCommand.Parameters.Add("@IdPerfil", SqlDbType.Int).Value = oPerfil.IdPerfil;
                sqlCommand.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = oPerfil.Nombre;
                sqlCommand.Parameters.Add("@Estado", SqlDbType.Bit).Value = oPerfil.Estado;
                sqlCommand.Parameters.Add("@UsuarioCreacion", SqlDbType.VarChar, 50).Value = oPerfil.UsuarioCreacion;
                sqlCommand.Parameters.Add("@EquipoCreacion", SqlDbType.VarChar, 50).Value = oPerfil.EquipoCreacion;
                sqlCommand.Parameters.Add("@UsuarioModificacion", SqlDbType.VarChar, 50).Value = oPerfil.UsuarioModificacion;
                sqlCommand.Parameters.Add("@EquipoModificacion", SqlDbType.VarChar, 50).Value = oPerfil.EquipoModificacion;
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
        public bool Actualizar(ObjPerfiles oPerfil)
        {
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpPerfiles";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = oPerfil.Operacion;
                sqlCommand.Parameters.Add("@IdPerfil", SqlDbType.Int).Value = oPerfil.IdPerfil;
                sqlCommand.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = oPerfil.Nombre;
                sqlCommand.Parameters.Add("@Estado", SqlDbType.Bit).Value = oPerfil.Estado;
                sqlCommand.Parameters.Add("@UsuarioModificacion", SqlDbType.VarChar, 50).Value = oPerfil.UsuarioModificacion;
                sqlCommand.Parameters.Add("@EquipoModificacion", SqlDbType.VarChar, 50).Value = oPerfil.EquipoModificacion;
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
        public bool GuardarModulosPerfil(ObjPerfiles oPerfil)
        {
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpPerfiles";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "CMP";
                sqlCommand.Parameters.Add("@IdPerfil", SqlDbType.Int).Value = oPerfil.IdPerfil;
                sqlCommand.Parameters.Add("@IdModulo", SqlDbType.Int).Value = oPerfil.IdModulo;
                sqlCommand.Parameters.Add("@Estado", SqlDbType.Bit).Value = oPerfil.TienePermiso;
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
        public DataTable ListarPerfiles()
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
        public DataTable ListarMaximoPerfil()
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
        public DataTable ListarPerfil(int IdPerfil)
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
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LP";
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
        public DataTable ListarModulosPerfil(int IdPerfil)
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
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LMP";
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
        public DataTable ListarPerfilPorNombre(string Nombre)
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
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LN";
                sqlCommand.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = Nombre;
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
