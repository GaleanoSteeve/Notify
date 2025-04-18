using CapaDatos;
using CapaObjetos;
using System.Data;

namespace CapaNegocios
{
    public class NegProyectos
    {
        DatProyectos objProyectos = new DatProyectos();

        public bool Eliminar(ObjProyectos oProyecto)
        {
            bool Resultado = objProyectos.Eliminar(oProyecto);
            return Resultado;
        }
        public bool Almacenar(ObjProyectos oProyecto)
        {
            bool Resultado = objProyectos.Almacenar(oProyecto);
            return Resultado;
        }
        
        //Listar
        public DataTable ListarProyectos()
        {
            DataTable dtDatos = objProyectos.ListarProyectos();
            return dtDatos;
        }
        public DataTable ListarMaximoIdProyecto()
        {
            DataTable dtDatos = objProyectos.ListarMaximoIdProyecto();
            return dtDatos;
        }
        public DataTable ExisteProyecto(ObjProyectos oProyecto)
        {
            DataTable dtDatos = objProyectos.ExisteProyecto(oProyecto);
            return dtDatos;
        }
        public DataTable ListarProyecto(ObjProyectos oProyecto)
        {
            DataTable dtDatos = objProyectos.ListarProyecto(oProyecto);
            return dtDatos;
        }
        public DataTable ListarProyectoManzanas(ObjProyectos oProyecto)
        {
            DataTable dtDatos = objProyectos.ListarProyectoManzanas(oProyecto);
            return dtDatos;
        }
    }
}