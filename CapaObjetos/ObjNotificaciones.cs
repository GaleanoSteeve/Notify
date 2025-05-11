using System;

namespace CapaObjetos
{
    public class ObjNotificaciones
    {
        private string _Operacion;
        private int _IdNotificacion;
        private int _IdClienteLote;
        private long _Documento;
        private string _Cliente;
        private DateTime _FechaPagoCuota;
        private string _MedioNotificacion;
        private string _Mensaje;
        private string _UsuarioCreacion;
        private DateTime _FechaCreacion;

        public string Operacion
        {
            get { return _Operacion; }
            set { _Operacion = value; }
        }
        public int IdNotificacion
        {
            get { return _IdNotificacion; }
            set { _IdNotificacion = value; }
        }
        public int IdClienteLote
        {
            get { return _IdClienteLote; }
            set { _IdClienteLote = value; }
        }
        public long Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }
        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }
        public DateTime FechaPagoCuota
        {
            get { return _FechaPagoCuota; ; }
            set { _FechaPagoCuota = value; }
        }
        public string MedioNotificacion
        {
            get { return _MedioNotificacion; }
            set { _MedioNotificacion = value; }
        }
        public string Mensaje
        {
            get { return _Mensaje; }
            set { _Mensaje = value; }
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
    }
}