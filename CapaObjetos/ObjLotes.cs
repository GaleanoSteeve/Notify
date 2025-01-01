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
        private int _Numero;
        private decimal _Area;
        private decimal _Valor;
        private decimal _CuotaInicial;
        private decimal _CuotaMensual;
        private DateTime _FechaPago;
        private int _IdEstado;
        private string _Estado;
        private int _IdTipoPago;
        private string _TipoPago;
        private int _IdBanco;
        private string _Banco;
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
        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public decimal Area
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
        public DateTime FechaPago
        {
            get { return _FechaPago; }
            set { _FechaPago = value; }
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
        public int IdTipoPago
        {
            get { return _IdTipoPago; }
            set { _IdTipoPago = value; }
        }
        public string TipoPago
        {
            get { return _TipoPago; }
            set { _TipoPago = value; }
        }
        public int IdBanco
        {
            get { return _IdBanco; }
            set { _IdBanco = value; }
        }
        public string Banco
        {
            get { return _Banco; }
            set { _Banco = value; }
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