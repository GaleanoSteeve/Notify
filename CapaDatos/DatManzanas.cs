using System;
using System.Data;
using CapaObjetos;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DatManzanas
    {
        public bool Eliminar(ObjManzanas oManzana)
        {
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpAdminManzanas";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "D";
                sqlCommand.Parameters.Add("@IdManzana", SqlDbType.Int).Value = oManzana.IdManzana;
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
        public bool Almacenar(ObjManzanas oManzana)
        {
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.Connection = DatConexionDB.ObtenerConexion();
                if (sqlCommand.Connection.State == ConnectionState.Closed)
                {
                    sqlCommand.Connection.Open();
                }
                sqlCommand.CommandText = "stpAdminManzanas";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "C";
                sqlCommand.Parameters.Add("@IdManzana", SqlDbType.Int).Value = oManzana.IdManzana;
                sqlCommand.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = oManzana.Nombre;
                sqlCommand.Parameters.Add("@IdProyecto", SqlDbType.Int).Value = oManzana.IdProyecto;
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
        public DataTable ListarManzanas()
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
                sqlCommand.CommandText = "stpAdminManzanas";
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
        public DataTable ListarComboProyectos()
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
                sqlCommand.CommandText = "stpAdminManzanas";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LCP";
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
        public DataTable ListarMaximoIdManzana()
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
                sqlCommand.CommandText = "stpAdminManzanas";
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
        public DataTable ExisteManzana(ObjManzanas oManzana)
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
                sqlCommand.CommandText = "stpAdminManzanas";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "EM";
                sqlCommand.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = oManzana.Nombre;
                sqlCommand.Parameters.Add("@IdProyecto", SqlDbType.Int).Value = oManzana.IdProyecto;
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
        public DataTable ListarManzana(ObjManzanas oManzana)
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
                sqlCommand.CommandText = "stpAdminManzanas";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LM";
                sqlCommand.Parameters.Add("@IdManzana", SqlDbType.Int).Value = oManzana.IdManzana;
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
        public DataTable ListarManzanaLotes(ObjManzanas oManzana)
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
                sqlCommand.CommandText = "stpAdminManzanas";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@Operacion", SqlDbType.VarChar, 4).Value = "LML";
                sqlCommand.Parameters.Add("@IdManzana", SqlDbType.Int).Value = oManzana.IdManzana;
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