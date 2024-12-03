﻿using System;
using CapaObjetos;
using System.Data;
using CapaCriptografia;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DatUsuarios
    {
        public bool PuedeEliminar(ObjUsuarios oUsuario)
        {
            bool Permiso = false;
            SqlDataReader sqlDataReader;
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpAdminUsuarios";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "UPE";
                sqlCommand.Parameters.Add("@Codigo", SqlDbType.Int).Value = oUsuario.Codigo;
                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Permiso = sqlDataReader.GetBoolean(0);
                }
                return Permiso;
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
        public bool EliminarUsuario(ObjUsuarios oUsuario)
        {
            bool Resultado = false;
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpAdminUsuarios";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "D";
                sqlCommand.Parameters.Add("@Codigo", SqlDbType.Int).Value = oUsuario.Codigo;
                sqlCommand.ExecuteReader();
                Resultado = true;
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
        public bool AlmacenarUsuario(ObjUsuarios oUsuario)
        {
            bool Resultado = false;
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpAdminUsuarios";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "C";
                sqlCommand.Parameters.Add("@Codigo", SqlDbType.Int).Value = oUsuario.Codigo;
                sqlCommand.Parameters.Add("@Nombres", SqlDbType.VarChar, 100).Value = oUsuario.Nombres;
                sqlCommand.Parameters.Add("@Usuario", SqlDbType.VarChar, 10).Value = oUsuario.Usuario;
                sqlCommand.Parameters.Add("@Contrasena", SqlDbType.VarChar, 200).Value = oUsuario.Contrasena;
                sqlCommand.Parameters.Add("@IdPerfil", SqlDbType.Int).Value = oUsuario.IdPerfil;
                sqlCommand.Parameters.Add("@Perfil", SqlDbType.VarChar, 50).Value = oUsuario.Perfil;
                sqlCommand.Parameters.Add("@PuedeEliminar", SqlDbType.Bit).Value = oUsuario.PuedeEliminar;
                sqlCommand.Parameters.Add("@Estado", SqlDbType.Bit).Value = oUsuario.Estado;
                sqlCommand.Parameters.Add("@UsuarioCreacion", SqlDbType.VarChar, 10).Value = oUsuario.UsuarioCreacion;
                sqlCommand.Parameters.Add("@EquipoCreacion", SqlDbType.VarChar, 50).Value = oUsuario.EquipoCreacion;
                sqlCommand.Parameters.Add("@UsuarioModificacion", SqlDbType.VarChar, 10).Value = oUsuario.UsuarioModificacion;
                sqlCommand.Parameters.Add("@EquipoModificacion", SqlDbType.VarChar, 50).Value = oUsuario.EquipoModificacion;
                sqlCommand.ExecuteReader();
                Resultado = true;
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

        //Listar
        public int ListarCodigo()
        {
            int Codigo = 0;
            SqlDataReader sqlDataReader;
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpAdminUsuarios";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "MAX";
                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Codigo = sqlDataReader.GetInt32(0);
                }
                return Codigo;
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
        public DataTable ListarComboUsuarios()
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
                sqlCommand.CommandText = "stpAdminUsuarios";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LCU";
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
        public DataSet ListarUsuario(ObjUsuarios oUsuario)
        {
            DataSet dsDatos = new DataSet();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpAdminUsuarios";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LU";
                sqlCommand.Parameters.Add("@Usuario", SqlDbType.VarChar, 20).Value = oUsuario.Usuario;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dsDatos);
                return dsDatos;
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
        public DataTable BuscarUsuario(ObjUsuarios oUsuario)
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
                sqlCommand.CommandText = "stpAdminUsuarios";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "R";
                sqlCommand.Parameters.Add("@Usuario", SqlDbType.VarChar, 10).Value = oUsuario.Usuario;
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
        public DataTable ExisteUsuario(ObjUsuarios oUsuario)
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
                sqlCommand.CommandText = "stpAdminUsuarios";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LEU";
                sqlCommand.Parameters.Add("@Usuario", SqlDbType.VarChar, 10).Value = oUsuario.Usuario;
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
        public DataTable ListarUsuarios(ObjUsuarios oUsuario)
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
                sqlCommand.CommandText = "stpAdminUsuarios";
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
        public DataTable ListarUsuarioCodigo(ObjUsuarios oUsuario)
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
                sqlCommand.CommandText = "stpAdminUsuarios";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LUC";
                sqlCommand.Parameters.Add("@Codigo", SqlDbType.Int).Value = oUsuario.Codigo;
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