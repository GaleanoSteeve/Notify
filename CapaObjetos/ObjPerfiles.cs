using System;

namespace CapaObjetos
{
    public class ObjPerfiles
    {
        private string _Operacion;
        private int _IdPerfil;
        private string _Nombre;
        private bool _Estado;
        private string _UsuarioCreacion;
        private DateTime _FechaCreacion;
        private string _UsuarioModificacion;
        private DateTime _FechaModificacion;

        public string Operacion
        {
            get { return _Operacion; }
            set { _Operacion = value; }
        }
        public int IdPerfil
        {
            get { return _IdPerfil; }
            set { _IdPerfil = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public bool Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        public string UsuarioCreacion
        {
            get { return _UsuarioCreacion; }
            set { _UsuarioCreacion = value; }
        }
        public DateTime FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }
        public string UsuarioModificacion
        {
            get { return _UsuarioModificacion; }
            set { _UsuarioModificacion = value; }
        }
        public DateTime FechaModificacion
        {
            get { return _FechaModificacion; }
            set { _FechaModificacion = value; }
        }
    }
}
