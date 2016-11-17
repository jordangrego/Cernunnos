using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CernunnosWeb.Security.Cryptography
{
    /// <summary>
    /// This class contains methods to en-/decrypt strings.
    /// </summary>
    public static class Encryption64
    {
        #region members

        /// <summary>
        /// Default encryption key.
        /// </summary>
        private const string DefaultKey = "#kl?+@<z";

        #endregion

        /// <summary>
        /// Encrypt string.
        /// </summary>
        /// <param name="stringToEncrypt">String to be encrypted.</param>
        /// <param name="key">Encryption key.</param>
        /// <returns>Encrypted string.</returns>
        public static string Encrypt(string stringToEncrypt, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream;

            // Check whether the key is valid, otherwise make it valid
            CheckKey(ref key);

            des.Key = HashKey(key, des.KeySize / 8);
            des.IV = HashKey(key, des.KeySize / 8);
            byte[] inputBytes = Encoding.UTF8.GetBytes(stringToEncrypt);

            cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();

            return Convert.ToBase64String(memoryStream.ToArray()).Replace("/", "-").Replace("+", "_");
        }

        /// <summary>
        /// Decrypt string.
        /// </summary>
        /// <param name="stringToDecrypt">String to be decrypted.</param>
        /// <param name="key">Encryption key.</param>
        /// <returns>Encrypted string.</returns>
        public static string Decrypt(string stringToDecrypt, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream;

            // Check whether the key is valid, otherwise make it valid
            CheckKey(ref key);

            des.Key = HashKey(key, des.KeySize / 8);
            des.IV = HashKey(key, des.KeySize / 8);
            byte[] inputBytes = Convert.FromBase64String(stringToDecrypt.Replace("-", "/").Replace("_", "+"));

            cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();

            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(memoryStream.ToArray());
        }

        /// <summary>
        /// Make sure the used key has a length of exact eight characters.
        /// </summary>
        /// <param name="keyToCheck">Key being checked.</param>
        private static void CheckKey(ref string keyToCheck)
        {
            keyToCheck = keyToCheck.Length > 8 ? keyToCheck.Substring(0, 8) : keyToCheck;
            if (keyToCheck.Length < 8)
            {
                for (int i = keyToCheck.Length; i < 8; i++)
                {
                    keyToCheck += DefaultKey[i];
                }
            }
        }

        /// <summary>
        /// Hash a key.
        /// </summary>
        /// <param name="key">Key being hashed.</param>
        /// <param name="length">Length of the output.</param>
        /// <returns>Hash key.</returns>
        private static byte[] HashKey(string key, int length)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            // Hash the key
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);

            // Truncate hash
            byte[] truncatedHash = new byte[length];
            Array.Copy(hash, 0, truncatedHash, 0, length);
            return truncatedHash;
        }
    }
}
