namespace CapaObjetos
{
    public class ObjProyectos
    {
        private string _Operacion;
        private int _IdProyecto;
        private string _Nombre;

        public string Operacion
        {
            get { return _Operacion; }
            set { _Operacion = value; }
        }
        public int IdProyecto
        {
            get { return _IdProyecto; }
            set { _IdProyecto = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
    }
}