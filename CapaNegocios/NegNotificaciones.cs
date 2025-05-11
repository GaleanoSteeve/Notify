using CapaDatos;
using System.Data;
using CapaObjetos;

namespace CapaNegocios
{
    public class NegNotificaciones
    {
        DatNotificaciones objNotificaciones = new DatNotificaciones();

        public bool Enviar(ObjNotificaciones oNotificaciones)
        {
            bool Resultado = objNotificaciones.Enviar(oNotificaciones);
            return Resultado;
        }

        //Listar
        public DataTable ListarRegistros()
        {
            DataTable dtDatos = objNotificaciones.ListarRegistros();
            return dtDatos;
        }
    }
}