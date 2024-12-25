using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class NegInformacionRegional
    {
        DatInformacionRegional objInformacionRegional = new DatInformacionRegional();


        public DataTable ListarComboPaises()
        {
            DataTable dtDatos = objInformacionRegional.ListarComboPaises();
            return dtDatos;
        }
        public DataTable ListarMunicipios()
        {
            DataTable dtDatos = objInformacionRegional.ListarMunicipios();
            return dtDatos;
        }
        public DataTable ListarComboDepartamentos()
        {
            DataTable dtDatos = objInformacionRegional.ListarComboDepartamentos();
            return dtDatos;
        }
        public DataTable ListarDepartamentosPorPais(int IdPais)
        {
            DataTable dtDatos = objInformacionRegional.ListarDepartamentosPorPais(IdPais);
            return dtDatos;
        }
        public DataTable ListarMunicipiosPorDepartamento(string IdDepartamento)
        {
            DataTable dtDatos = objInformacionRegional.ListarMunicipiosPorDepartamento(IdDepartamento);
            return dtDatos;
        }
    }
}
