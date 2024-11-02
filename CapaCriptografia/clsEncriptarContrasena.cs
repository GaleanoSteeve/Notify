using System.Text;
using System.Security.Cryptography;

namespace CapaCriptografia
{
    public class clsEncriptarContrasena
    {
        public string EncriptarContrasena(string Cadena)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;

            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(Cadena));

            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
        }
    }
}