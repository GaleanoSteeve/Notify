using System;

namespace CapaObjetos
{
    public class ObjLotes
    {
        private string _Operacion;
        private int _IdLote;
        private int _IdProyecto;
        private string _Proyecto;
        private int _IdManzana;
        private string _Manzana;
        private int _NumeroLote;
        private int _Area;
        private decimal _Valor;
        private decimal _CuotaInicial;
        private decimal _CuotaMensual;
        private DateTime _FechaInicioPagoCuotas;
        private int _DiaPagoCuota;
        private int _IdEstado;
        private string _Estado;
        private string _UsuarioCreacion;
        private DateTime _FechaCreacion;
        private string _UsuarioModificacion;
        private DateTime _FechaModificacion;
        private string _Filtro;

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
        public int IdProyecto
        {
            get { return _IdProyecto; }
            set { _IdProyecto = value; }
        }
        public string Proyecto
        {
            get { return _Proyecto; }
            set { _Proyecto = value; }
        }
        public int IdManzana
        {
            get { return _IdManzana; }
            set { _IdManzana = value; }
        }
        public string Manzana
        {
            get { return _Manzana; }
            set { _Manzana = value; }
        }
        public int NumeroLote
        {
            get { return _NumeroLote; }
            set { _NumeroLote = value; }
        }
        public int Area
        {
            get { return _Area; }
            set { _Area = value; }
        }
        public decimal Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
        public decimal CuotaInicial
        {
            get { return _CuotaInicial; }
            set { _CuotaInicial = value; }
        }
        public decimal CuotaMensual
        {
            get { return _CuotaMensual; }
            set { _CuotaMensual = value; }
        }
        public DateTime FechaInicioPagoCuotas
        {
            get { return _FechaInicioPagoCuotas; }
            set { _FechaInicioPagoCuotas = value; }
        }
        public int DiaPagoCuota
        {
            get { return _DiaPagoCuota; }
            set { _DiaPagoCuota = value; }
        }
        public int IdEstado
        {
            get { return _IdEstado; }
            set { _IdEstado = value; }
        }
        public string Estado
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
        public string Filtro
        {
            get { return _Filtro; }
            set { _Filtro = value; }
        }
    }
}