using CapaCriptografia;

namespace CapaNegocios
{
    public class NegCriptografia
    {
        clsEncriptarContrasena objEncriptarContrasena = new clsEncriptarContrasena();

        public string EncriptarContrasena(string Cadena)
        {
            string Resultado = objEncriptarContrasena.EncriptarContrasena(Cadena);
            return Resultado;
        }
    }
}