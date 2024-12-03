using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class NegInformacionRegional
    {
        DatInformacionRegional objInformacionRegional = new DatInformacionRegional();

        public DataTable ListarCiudades()
        {
            DataTable dtDatos = objInformacionRegional.ListarCiudades();
            return dtDatos;
        }
        public DataTable ListarComboDepartamentos()
        {
            DataTable dtDatos = objInformacionRegional.ListarComboDepartamentos();
            return dtDatos;
        }
        public DataTable ListarCiudadPorDepartamento(string IdDepartamento)
        {
            DataTable dtDatos = objInformacionRegional.ListarCiudadPorDepartamento(IdDepartamento);
            return dtDatos;
        }
    }
}
