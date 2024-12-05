using CapaDatos;
using CapaObjetos;
using System.Data;

namespace CapaNegocios
{
    public class NegConfiguracion
    {
        DatConfiguracion objConfiguracion = new DatConfiguracion();

        //Listar
        public DataTable ListarConfiguracion()
        {
            DataTable dtDatos = objConfiguracion.ListarConfiguracion();
            return dtDatos;
        }

        public string Guardar(ObjConfiguracion oConfiguracion)
        {
            string Resultado = objConfiguracion.Guardar(oConfiguracion);
            return Resultado;
        }
    }
}
