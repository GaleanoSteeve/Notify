using CapaDatos;
using CapaObjetos;
using System.Data;

namespace CapaNegocios
{
    public class NegClientes
    {
        DatClientes objClientes = new DatClientes();

        public bool Eliminar(ObjClientes oCliente)
        {
            bool Resultado = objClientes.Eliminar(oCliente);
            return Resultado;
        }
        public bool Almacenar(ObjClientes oCliente)
        {
            bool Resultado = objClientes.Almacenar(oCliente);
            return Resultado;
        }

        //Listar
        public DataTable ListarClientes()
        {
            DataTable dtDatos = objClientes.ListarClientes();
            return dtDatos;
        }
        public DataTable ListarMaximoIdCliente()
        {
            DataTable dtDatos = objClientes.ListarMaximoIdCliente();
            return dtDatos;
        }
        public DataTable ListarComboTipoDocumentos()
        {
            DataTable dtDatos = objClientes.ListarComboTipoDocumentos();
            return dtDatos;
        }
        public DataTable ListarCliente(long Documento)
        {
            DataTable dtDatos = objClientes.ListarCliente(Documento);
            return dtDatos;
        }
        public DataTable ExisteDocumento(ObjClientes oCliente)
        {
            DataTable dtDatos = objClientes.ExisteDocumento(oCliente);
            return dtDatos;
        }
        public DataTable ListarClienteParametros(string Filtro)
        {
            DataTable dtDatos = objClientes.ListarClienteParametros(Filtro);
            return dtDatos;
        }
    }
}