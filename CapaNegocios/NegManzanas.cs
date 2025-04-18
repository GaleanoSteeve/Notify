using CapaDatos;
using CapaObjetos;
using System.Data;

namespace CapaNegocios
{
    public class NegManzanas
    {
        DatManzanas objManzanas = new DatManzanas();

        public bool Eliminar(ObjManzanas oManzana)
        {
            bool Resultado = objManzanas.Eliminar(oManzana);
            return Resultado;
        }
        public bool Almacenar(ObjManzanas oManzana)
        {
            bool Resultado = objManzanas.Almacenar(oManzana);
            return Resultado;
        }

        //Listar
        public DataTable ListarManzanas()
        {
            DataTable dtDatos = objManzanas.ListarManzanas();
            return dtDatos;
        }
        public DataTable ListarComboProyectos()
        {
            DataTable dtDatos = objManzanas.ListarComboProyectos();
            return dtDatos;
        }
        public DataTable ListarMaximoIdManzana()
        {
            DataTable dtDatos = objManzanas.ListarMaximoIdManzana();
            return dtDatos;
        }
        public DataTable ExisteManzana(ObjManzanas oManzana)
        {
            DataTable dtDatos = objManzanas.ExisteManzana(oManzana);
            return dtDatos;
        }
        public DataTable ListarManzana(ObjManzanas oManzana)
        {
            DataTable dtDatos = objManzanas.ListarManzana(oManzana);
            return dtDatos;
        }
        public DataTable ListarManzanaLotes(ObjManzanas oManzana)
        {
            DataTable dtDatos = objManzanas.ListarManzanaLotes(oManzana);
            return dtDatos;
        }
    }
}