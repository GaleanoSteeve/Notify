namespace CapaObjetos
{
    public class ObjManzanas
    {
        private string _Operacion;
        private int _IdManzana;
        private string _Nombre;
        private int _IdProyecto;

        public string Operacion
        {
            get { return _Operacion; }
            set { _Operacion = value; }
        }
        public int IdManzana
        {
            get { return _IdManzana; }
            set { _IdManzana = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public int IdProyecto
        {
            get { return _IdProyecto; }
            set { _IdProyecto = value; }
        }
    }
}