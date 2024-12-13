using CapaCriptografia;

namespace CapaNegocios
{
    public class NegCriptografia
    {
        clsCriptografia objCriptografia = new clsCriptografia();

        public string EncriptarContrasena(string Cadena)
        {
            string Resultado = objCriptografia.EncryptToString(Cadena);
            return Resultado;
        }
        public string DesencriptarContrasena(string Cadena)
        {
            string Resultado = objCriptografia.DecryptString(Cadena);
            return Resultado;
        }
    }
}