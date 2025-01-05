using CapaDatos;
using CapaObjetos;
using System.Data;

namespace CapaNegocios
{
    public class NegClientesLotes
    {
        DatClientesLotes objClientesLotes = new DatClientesLotes();

        public bool Eliminar(ObjClientesLotes oClienteLote)
        {
            bool Resultado = objClientesLotes.Eliminar(oClienteLote);
            return Resultado;
        }
        public bool Almacenar(ObjClientesLotes oClienteLote)
        {
            bool Resultado = objClientesLotes.Almacenar(oClienteLote);
            return Resultado;
        }

        //Listar
        public DataSet ListarClienteLote(ObjClientesLotes oClienteLote)
        {
            DataSet dsDatos = objClientesLotes.ListarClienteLote(oClienteLote);
            return dsDatos;
        }
    }
}