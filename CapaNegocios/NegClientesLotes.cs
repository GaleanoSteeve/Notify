using CapaDatos;
using CapaObjetos;
using System.Data;

namespace CapaNegocios
{
    public class NegClientesLotes
    {
        DatClientesLotes objClientesLotes = new DatClientesLotes();

        public bool EliminarLotes(long Documento)
        {
            bool Resultado = objClientesLotes.EliminarLotes(Documento);
            return Resultado;
        }
        public bool Guardar(ObjClientesLotes oClienteLote)
        {
            bool Resultado = objClientesLotes.Guardar(oClienteLote);
            return Resultado;
        }
        public bool EliminarLote(ObjClientesLotes oClienteLote)
        {
            bool Resultado = objClientesLotes.EliminarLote(oClienteLote);
            return Resultado;
        }

        //Listar
        public DataSet Listar(ObjClientesLotes oClienteLote)
        {
            DataSet dsDatos = objClientesLotes.Listar(oClienteLote);
            return dsDatos;
        }
    }
}