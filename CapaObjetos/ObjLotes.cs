using System;
namespace CapaObjetos
{
    public class ObjLotes
    {
        private string _Operacion;
        private int _IdLote;
        private string _Ubicacion;
        private decimal _Area;
        private decimal _Precio;
        private int _IdEstado;
        private string _UsuarioCreacion;
        private DateTime _FechaCreacion;
        private string _EquipoCreacion;
        private string _UsuarioModificacion;
        private DateTime _FechaModificacion;
        private string _EquipoModificacion;

        public string Operacion
        {
            get { return _Operacion; }
            set { _Operacion = value; }
        }
        public int IdLote
        {
            get { return _IdLote; }
            set { _IdLote = value; }
        }
        public string Ubicacion
        {
            get { return _Ubicacion; }
            set { _Ubicacion = value; }
        }
        public decimal Area
        {
            get { return _Area; }
            set { _Area = value; }
        }
        public decimal Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }
        public int IdEstado
        {
            get { return _IdEstado; }
            set { _IdEstado = value; }
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
        public string EquipoCreacion
        {
            get { return _EquipoCreacion; }
            set { _EquipoCreacion = value; }
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
        public string EquipoModificacion
        {
            get { return _EquipoModificacion; }
            set { _EquipoModificacion = value; }
        }
    }
}
