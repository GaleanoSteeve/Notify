using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace CapaCriptografia
{
    public class clsCriptografia
    {
        #region Variables

        private System.Text.UTF8Encoding UTFEncoder;
        private ICryptoTransform EncryptorTransform, DecryptorTransform;

        private byte[] Key = { 17, 27, 181, 17, 98, 29, 9, 4, 164, 184, 26, 12, 146, 92, 201, 29, 21, 41, 213, 174, 83, 121, 16, 29, 124, 26, 37, 208, 131, 153, 167, 20 };
        private byte[] Vector = { 106, 174, 8, 95, 203, 223, 183, 11, 41, 10, 25, 205, 19, 131, 85, 15 };

        #endregion

        #region Constructor

        public clsCriptografia()
        {
            //This is our encryption method
            RijndaelManaged rm = new RijndaelManaged();

            //Create an encryptor and a decryptor using our encryption method, key, and vector.
            EncryptorTransform = rm.CreateEncryptor(this.Key, this.Vector);
            DecryptorTransform = rm.CreateDecryptor(this.Key, this.Vector);

            //Used to translate bytes to text and vice versa
            UTFEncoder = new System.Text.UTF8Encoding();
        }

        #endregion

        //Generates an encryption key:
        static public byte[] GenerateEncryptionKey()
        {
            //Generate a Key.
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateKey();
            return rm.Key;
        }

        //Generates a unique encryption vector:
        static public byte[] GenerateEncryptionVector()
        {
            //Generate a Vector
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateIV();
            return rm.IV;
        }

        //Encrypt some text and return a string suitable for passing in a URL.
        public string EncryptToString(string TextValue)
        {
            return ByteArrToString(Encrypt(TextValue));
        }

        //Encrypt some text and return an encrypted byte array.
        public byte[] Encrypt(string TextValue)
        {
            //Translates our text value into a byte array.
            Byte[] bytes = UTFEncoder.GetBytes(TextValue);

            //Used to stream the data in and out of the CryptoStream.
            MemoryStream memoryStream = new MemoryStream();

            // We will have to write the unencrypted bytes to the stream, then read the encrypted result back from the stream

            #region Write the decrypted value to the encryption stream
            CryptoStream cs = new CryptoStream(memoryStream, EncryptorTransform, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            #endregion

            #region Read encrypted value back out of the stream
            memoryStream.Position = 0;
            byte[] encrypted = new byte[memoryStream.Length];
            memoryStream.Read(encrypted, 0, encrypted.Length);
            #endregion

            //Clean up.
            cs.Close();
            memoryStream.Close();

            return encrypted;
        }

        //The other side: Decryption methods
        public string DecryptString(string EncryptedString)
        {
            return Decrypt(StrToByteArray(EncryptedString));
        }

        //Decryption when working with byte arrays
        public string Decrypt(byte[] EncryptedValue)
        {
            #region Write the encrypted value to the decryption stream

            MemoryStream encryptedStream = new MemoryStream();
            CryptoStream decryptStream = new CryptoStream(encryptedStream, DecryptorTransform, CryptoStreamMode.Write);
            decryptStream.Write(EncryptedValue, 0, EncryptedValue.Length);
            decryptStream.FlushFinalBlock();

            #endregion

            #region Read the decrypted value from the stream.

            encryptedStream.Position = 0;
            Byte[] decryptedBytes = new Byte[encryptedStream.Length];
            encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
            encryptedStream.Close();

            #endregion

            return UTFEncoder.GetString(decryptedBytes);
        }

        public byte[] StrToByteArray(string str)
        {
            if (str.Length == 0)
                throw new Exception("Invalid string value in StrToByteArray");

            byte val;
            byte[] byteArr = new byte[str.Length / 3];
            int i = 0;
            int j = 0;
            do
            {
                val = byte.Parse(str.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            }
            while (i < str.Length);
            return byteArr;
        }

        public string ByteArrToString(byte[] byteArr)
        {
            byte val;
            string tempStr = "";
            for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
            {
                val = byteArr[i];
                if (val < (byte)10)
                    tempStr += "00" + val.ToString();
                else if (val < (byte)100)
                    tempStr += "0" + val.ToString();
                else
                    tempStr += val.ToString();
            }
            return tempStr;
        }

        //Contrasena
        public string EncryptPassword(string Cadena)
        {
            try
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
            catch (Exception ex)
            {                
                throw ex;
            }            
        }
    }
}