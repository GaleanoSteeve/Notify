using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class NegRegionales
    {
        DatRegionales objRegionales = new DatRegionales();

        public DataTable ListarComboPaises()
        {
            DataTable dtDatos = objRegionales.ListarComboPaises();
            return dtDatos;
        }
        public DataTable ListarComboDepartamentos()
        {
            DataTable dtDatos = objRegionales.ListarComboDepartamentos();
            return dtDatos;
        }
        public DataTable ListarComboDepartamentosPais(int IdPais)
        {
            DataTable dtDatos = objRegionales.ListarComboDepartamentosPais(IdPais);
            return dtDatos;
        }
        public DataTable ListarComboCorregimientosMunicipio(int IdMunicipio)
        {
            DataTable dtDatos = objRegionales.ListarComboCorregimientosMunicipio(IdMunicipio);
            return dtDatos;
        }
        public DataTable ListarComboMunicipiosDepartamento(int IdDepartamento)
        {
            DataTable dtDatos = objRegionales.ListarComboMunicipiosDepartamento(IdDepartamento);
            return dtDatos;
        }
        public DataTable ListarComboVeredasCorregimientos(int IdCorregimiento)
        {
            DataTable dtDatos = objRegionales.ListarComboVeredasCorregimientos(IdCorregimiento);
            return dtDatos;
        }
    }
}
