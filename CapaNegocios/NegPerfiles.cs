using CapaDatos;
using CapaObjetos;
using System.Data;

namespace CapaNegocios
{
    public class NegPerfiles
    {
        DatPerfiles objPerfiles = new DatPerfiles();

        public bool Guardar(ObjPerfiles oPerfil)
        {
            bool Resultado = objPerfiles.Guardar(oPerfil);
            return Resultado;
        }
        public bool GuardarModulosPerfil(ObjModulos oModulo)
        {
            bool Resultado = objPerfiles.GuardarModulosPerfil(oModulo);
            return Resultado;
        }
        public bool EliminarModulosPerfil(ObjPerfiles oPerfil)
        {
            bool Resultado = objPerfiles.EliminarModulosPerfil(oPerfil);
            return Resultado;
        }

        //Listar
        public DataTable ListarPerfiles()
        {
            DataTable dtDatos = objPerfiles.ListarPerfiles();
            return dtDatos;
        }
        public DataTable ListarMaximoPerfil()
        {
            DataTable dtDatos = objPerfiles.ListarMaximoPerfil();
            return dtDatos;
        }
        public DataTable ListarComboPerfiles()
        {
            DataTable dtDatos = objPerfiles.ListarComboPerfiles();
            return dtDatos;
        }
        public DataTable ListarPerfil(int IdPerfil)
        {
            DataTable dtDatos = objPerfiles.ListarPerfil(IdPerfil);
            return dtDatos;
        }
        public DataTable ListarModulosPerfil(int IdPerfil)
        {
            DataTable dtDatos = objPerfiles.ListarModulosPerfil(IdPerfil);
            return dtDatos;
        }
        public DataTable ListarPerfilPorNombre(string Nombre)
        {
            DataTable dtDatos = objPerfiles.ListarPerfilPorNombre(Nombre);
            return dtDatos;
        }
    }
}
