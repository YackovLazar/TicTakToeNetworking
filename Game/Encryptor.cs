using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class Encryptor
    {
        private static readonly byte[] Key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
        private static readonly byte[] IV = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };

        internal static string Encrypt(string plainText)
        {
            byte[] encrypted;
            using (var rijndaelManaged = new System.Security.Cryptography.RijndaelManaged())
            {
                rijndaelManaged.Key = Key;
                rijndaelManaged.IV = IV;
                var encryptor = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    using (var cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new System.IO.StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        encrypted = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }

        internal static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            string decrypted;
            using (var rijndaelManaged = new System.Security.Cryptography.RijndaelManaged())
            {
                rijndaelManaged.Key = Key;
                rijndaelManaged.IV = IV;
                var decryptor = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                using (var memoryStream = new System.IO.MemoryStream(cipherBytes))
                {
                    using (var cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                    {
                        using (var streamReader = new System.IO.StreamReader(cryptoStream))
                        {
                            decrypted = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return decrypted;
        }
    }
}
