using System;

namespace CapaObjetos
{
    public class ObjClientesLotes
    {
        private string _Operacion;
        private int _IdClienteLote;
        private long _Documento;
        private int _IdProyecto;
        private int _IdManzana;
        private int _IdLote;
        private int _Numero;
        private string _UsuarioCreacion;
        private DateTime _FechaCreacion;
        private string _UsuarioModificacion;
        private DateTime _FechaModificacion;

        public string Operacion
        {
            get { return _Operacion; }
            set { _Operacion = value; }
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
        public int IdProyecto
        {
            get { return _IdProyecto; }
            set { _IdProyecto = value; }
        }
        public int IdManzana
        {
            get { return _IdManzana; }
            set { _IdManzana = value; }
        }
        public int IdLote
        {
            get { return _IdLote; }
            set { _IdLote = value; }
        }
        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
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