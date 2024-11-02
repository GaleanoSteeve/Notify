using CapaDatos;
using CapaObjetos;
using System.Data;

namespace CapaNegocios
{
    public class NegUsuarios
    {
        DatUsuarios objUsuarios = new DatUsuarios();

        public bool PuedeEliminar(ObjUsuarios oUsuario)
        {
            bool Permiso = objUsuarios.PuedeEliminar(oUsuario);
            return Permiso;
        }
        public bool EliminarUsuario(ObjUsuarios oUsuario)
        {
            bool Resultado = objUsuarios.EliminarUsuario(oUsuario);
            return Resultado;
        }
        public bool AlmacenarUsuario(ObjUsuarios oUsuario)
        {
            bool Resultado = objUsuarios.AlmacenarUsuario(oUsuario);
            return Resultado;
        }

        //Listar
        public int ListarCodigo()
        {
            int Codigo = objUsuarios.ListarCodigo();
            return Codigo;
        }
        public DataTable ListarComboUsuarios()
        {
            DataTable dtDatos = objUsuarios.ListarComboUsuarios();
            return dtDatos;
        }
        public DataSet ListarUsuario(ObjUsuarios oUsuario)
        {
            DataSet dsDatos = objUsuarios.ListarUsuario(oUsuario);
            return dsDatos;
        }
        public DataTable BuscarUsuario(ObjUsuarios oUsuario)
        {
            DataTable dtDatos = objUsuarios.BuscarUsuario(oUsuario);
            return dtDatos;
        }
        public DataTable ExisteUsuario(ObjUsuarios oUsuario)
        {
            DataTable dtDatos = objUsuarios.ExisteUsuario(oUsuario);
            return dtDatos;
        }
        public DataTable ListarUsuarios(ObjUsuarios oUsuario)
        {
            DataTable dtDatos = objUsuarios.ListarUsuarios(oUsuario);
            return dtDatos;
        }
        public DataTable ListarUsuarioCodigo(ObjUsuarios oUsuario)
        {
            DataTable dtDatos = objUsuarios.ListarUsuarioCodigo(oUsuario);
            return dtDatos;
        }
    }
}
