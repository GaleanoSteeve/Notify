using System;

namespace CapaObjetos
{
    public class ObjClientes
    {
        private string _Operacion;
        private int _IdCliente;
        private int _TipoDocumento;
        private long _Documento;
        private string _Nombres;
        private string _Apellidos;
        private string _Direccion;
        private long _Telefono;
        private string _Email;
        private bool _Estado;
        private string _UsuarioCreacion;
        private string _EquipoCreacion;
        private DateTime _FechaCreacion;
        private string _UsuarioModificacion;
        private string _EquipoModificacion;
        private DateTime _FechaModificacion;
        private string _Filtro;

        public string Operacion
        {
            get { return _Operacion; }
            set { _Operacion = value; }
        }
        public int IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        public int TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }
        public long Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }
        public string Nombres
        {
            get { return _Nombres; }
            set { _Nombres = value; }
        }
        public string Apellidos
        {
            get { return _Apellidos; }
            set { _Apellidos = value; }
        }
        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }
        public long Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
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
        public string EquipoCreacion
        {
            get { return _EquipoCreacion; }
            set { _EquipoCreacion = value; }
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
        public string EquipoModificacion
        {
            get { return _EquipoModificacion; }
            set { _EquipoModificacion = value; }
        }
        public DateTime FechaModificacion
        {
            get { return _FechaModificacion; }
            set { _FechaModificacion = value; }
        }
        public string Filtro
        {
            get { return _Filtro; }
            set { _Filtro = value; }
        }
    }
}