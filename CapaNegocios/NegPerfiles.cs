
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class NegPerfiles
    {
        DatPerfiles objPerfiles = new DatPerfiles();

        //Listar
        public DataTable ListarPerfiles()
        {
            DataTable dtDatos = objPerfiles.ListarPerfiles();
            return dtDatos;
        }

        //public DataTable ListarPerfilesActivos()
        //{
        //    DataTable dtDatos = objPerfiles.ListarPerfilesActivos();
        //    return dtDatos;
        //}
        //public DataTable BuscarPerfil(int IdPerfil)
        //{
        //    DataTable dtDatos = objPerfiles.BuscarPerfil(IdPerfil);
        //    return dtDatos;
        //}
        //public DataTable ListarPermisos(int IdPerfil)
        //{
        //    DataTable dtDatos = objPerfiles.ListarPermisos(IdPerfil);
        //    return dtDatos;
        //}

        //public int MaximoIdPerfil()
        //{
        //    int IdPerfil = 0;
        //    return IdPerfil = objPerfiles.MaximoIdPerfil();
        //}
        //public bool GuardarPerfil(ObjPerfiles oPerfil)
        //{
        //    return objPerfiles.GuardarPerfil(oPerfil);
        //}
        //public bool GuardarModulosPerfil(ObjPerfiles oPerfil)
        //{
        //    return objPerfiles.GuardarModulosPerfil(oPerfil);
        //}
        //public bool ModificarPerfil(ObjPerfiles oPerfil)
        //{
        //    return objPerfiles.ModificarPerfil(oPerfil);
        //}
    }
}
