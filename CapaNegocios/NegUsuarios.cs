using CapaDatos;
using CapaObjetos;
using System.Data;

namespace CapaNegocios
{
    public class NegUsuarios
    {
        DatUsuarios objUsuarios = new DatUsuarios();

        public bool Eliminar(ObjUsuarios oUsuario)
        {
            bool Resultado = objUsuarios.Eliminar(oUsuario);
            return Resultado;
        }
        public bool Almacenar(ObjUsuarios oUsuario)
        {
            bool Resultado = objUsuarios.Almacenar(oUsuario);
            return Resultado;
        }
        public bool PuedeEliminar(ObjUsuarios oUsuario)
        {
            bool Permiso = objUsuarios.PuedeEliminar(oUsuario);
            return Permiso;
        }

        //Listar
        public DataTable ListarUsuarios()
        {
            DataTable dtDatos = objUsuarios.ListarUsuarios();
            return dtDatos;
        }
        public DataTable ListarMaximoCodigo()
        {
            DataTable dtDatos = objUsuarios.ListarMaximoCodigo();
            return dtDatos;
        }
        public DataTable ListarComboUsuarios()
        {
            DataTable dtDatos = objUsuarios.ListarComboUsuarios();
            return dtDatos;
        }
        public DataTable ListarUsuarioCodigo(int Codigo)
        {
            DataTable dtDatos = objUsuarios.ListarUsuarioCodigo(Codigo);
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
    }
}
