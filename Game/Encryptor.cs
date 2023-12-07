using System;
using System.Security.Cryptography;

namespace Game
{
    internal class Encryptor
    {
        private static readonly byte[] Key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
        private static readonly byte[] IV = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08};

        internal static byte[] Encrypt(byte[] data)
        {
            using (SymmetricAlgorithm algo = Aes.Create())
            {
                algo.Padding = PaddingMode.None;
                using (ICryptoTransform encryptor = algo.CreateEncryptor(Key, IV))
                    return encryptor.TransformFinalBlock(data, 0, data.Length);
            }

        }

        internal static byte[] Decrypt(byte[] cipherText)
        {
            using (SymmetricAlgorithm algo = Aes.Create())
            {
                algo.Padding = PaddingMode.None;
                using (ICryptoTransform decryptor = algo.CreateDecryptor(Key, IV))
                {
                    return decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                }
            }
        }
    }
}
