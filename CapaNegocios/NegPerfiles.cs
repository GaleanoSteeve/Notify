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
        public bool Actualizar(ObjPerfiles oPerfil)
        {
            bool Resultado = objPerfiles.Actualizar(oPerfil);
            return Resultado;
        }
        public bool GuardarModulosPerfil(ObjPerfiles oPerfil)
        {
            bool Resultado = objPerfiles.GuardarModulosPerfil(oPerfil);
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
