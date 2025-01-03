using System;

namespace CapaObjetos
{
    public class ObjUsuarios
    {
        private string strOperacion;
        private int intCodigo;
        private string strNombres;
        private string strUsuario;
        private string strContrasena;
        private int intIdPerfil;
        private string strPerfil;
        private bool blnPuedeEliminar;
        private bool blnEstado;
        private string strUsuarioCreacion;
        private DateTime dtmFechaCreacion;
        private string strUsuarioModificacion;
        private DateTime dtmFechaModificacion;

        public string Operacion
        {
            get { return strOperacion; }
            set { strOperacion = value; }
        }
        public int Codigo
        {
            get { return intCodigo; }
            set { intCodigo = value; }
        }
        public string Nombres
        {
            get { return strNombres; }
            set { strNombres = value; }
        }
        public string Usuario
        {
            get { return strUsuario; }
            set { strUsuario = value; }
        }
        public string Contrasena
        {
            get { return strContrasena; }
            set { strContrasena = value; }
        }
        public int IdPerfil
        {
            get { return intIdPerfil; }
            set { intIdPerfil = value; }
        }
        public string Perfil
        {
            get { return strPerfil; }
            set { strPerfil = value; }
        }
        public bool PuedeEliminar
        {
            get { return blnPuedeEliminar; }
            set { blnPuedeEliminar = value; }
        }
        public bool Estado
        {
            get { return blnEstado; }
            set { blnEstado = value; }
        }
        public string UsuarioCreacion
        {
            get { return strUsuarioCreacion; }
            set { strUsuarioCreacion = value; }
        }
        public DateTime FechaCreacion
        {
            get { return dtmFechaCreacion; }
            set { dtmFechaCreacion = value; }
        }
        public string UsuarioModificacion
        {
            get { return strUsuarioModificacion; }
            set { strUsuarioModificacion = value; }
        }
        public DateTime FechaModificacion
        {
            get { return dtmFechaModificacion; }
            set { dtmFechaModificacion = value; }
        }
    }
}