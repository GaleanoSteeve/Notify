using System;
using CapaCriptografia;
using System.Configuration;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DatConexionDB
    {
        #region Variables

        private static string CadenaConexion = "";
        private static clsCriptografia objDesencriptar = new clsCriptografia();

        #endregion

        //Metodos
        private static string LeerCadenaConexion()
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString;
                CadenaConexion = objDesencriptar.DecryptString(CadenaConexion);
                return CadenaConexion;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public static SqlConnection ObtenerConexion()
        {
            try
            {
                CadenaConexion = LeerCadenaConexion();
                return new SqlConnection(CadenaConexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
