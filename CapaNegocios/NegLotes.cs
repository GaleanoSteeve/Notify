using CapaDatos;
using CapaObjetos;
using System.Data;

namespace CapaNegocios
{
    public class NegLotes
    {
        DatLotes objLotes = new DatLotes();

        public bool Eliminar(ObjLotes oLote)
        {
            bool Resultado = objLotes.Eliminar(oLote);
            return Resultado;
        }
        public bool Almacenar(ObjLotes oLote)
        {
            bool Resultado = objLotes.Almacenar(oLote);
            return Resultado;
        }

        //Listar
        public DataTable ListarLotes()
        {
            DataTable dtDatos = objLotes.ListarLotes();
            return dtDatos;
        }
        public DataTable ListarComboEstados()
        {
            DataTable dtDatos = objLotes.ListarComboEstados();
            return dtDatos;
        }
        public DataTable ListarMaximoIdLote()
        {
            DataTable dtDatos = objLotes.ListarMaximoIdLote();
            return dtDatos;
        }
        public DataTable ListarComboManzanas()
        {
            DataTable dtDatos = objLotes.ListarComboManzanas();
            return dtDatos;
        }
        public DataTable ListarComboProyectos()
        {
            DataTable dtDatos = objLotes.ListarComboProyectos();
            return dtDatos;
        }
        public DataTable ListarLote(int Codigo)
        {
            DataTable dtDatos = objLotes.ListarLote(Codigo);
            return dtDatos;
        }
        public DataTable ExisteLote(ObjLotes oLote)
        {
            DataTable dtDatos = objLotes.ExisteLote(oLote);
            return dtDatos;
        }
        public DataTable ListarLotesParametros(string Parametro)
        {
            DataTable dtDatos = objLotes.ListarLotesParametros(Parametro);
            return dtDatos;
        }
    }
}