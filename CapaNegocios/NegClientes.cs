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
        public DataTable ListarDatosReporte()
        {
            DataTable dtDatos = objClientes.ListarDatosReporte();
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
        public DataSet ListarCliente(long Documento)
        {
            DataSet dsDatos = objClientes.ListarCliente(Documento);
            return dsDatos;
        }
        public DataTable ExisteDocumento(ObjClientes oCliente)
        {
            DataTable dtDatos = objClientes.ExisteDocumento(oCliente);
            return dtDatos;
        }
        public DataTable ListarClientesParametros(string Parametro)
        {
            DataTable dtDatos = objClientes.ListarClientesParametros(Parametro);
            return dtDatos;
        }
    }
}