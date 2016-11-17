using System.Collections.Specialized;
using System.Text;

namespace CernunnosWeb.Security.Cryptography
{
    /// <summary>
    /// This class contains methods to en-/decrypt query strings.
    /// </summary>
    public static class CryptoQueryStringHandler
    {
        /// <summary>
        /// Parameter name with encryption.
        /// </summary>
        public static readonly string ParameterName = "af92f3585f63";

        /// <summary>
        /// Encrypt query strings from string.
        /// </summary>
        /// <param name="unencryptedStrings">Unencrypted query strings in the format 'param=value'.</param>
        /// <param name="key">Key, being used to encrypt.</param>
        /// <returns>Encrypted string.</returns>
        public static string EncryptQueryStrings(string unencryptedStrings, string key)
        {
            return ParameterName + Encryption64.Encrypt(unencryptedStrings, key);
        }

        /// <summary>
        /// Encrypt query strings from string array.
        /// </summary>
        /// <param name="unencryptedStrings">Unencrypted query strings in the format 'param=value'.</param>
        /// <param name="key">Key, being used to encrypt.</param>
        /// <returns>Encrypted string.</returns>
        public static string EncryptQueryStrings(string[] unencryptedStrings, string key)
        {
            StringBuilder strings = new StringBuilder();

            foreach (string unencryptedString in unencryptedStrings)
            {
                if (strings.Length > 0)
                {
                    strings.Append("&");
                }

                strings.Append(unencryptedString);
            }

            return Encryption64.Encrypt(strings.ToString(), key);
        }

        /// <summary>
        /// Encrypt query strings from name value collection.
        /// </summary>
        /// <param name="unencryptedStrings">Unencrypted query strings.</param>
        /// <param name="key">Key, being used to encrypt.</param>
        /// <returns>Encrypted string.</returns>
        public static string EncryptQueryStrings(NameValueCollection unencryptedStrings, string key)
        {
            StringBuilder strings = new StringBuilder();

            foreach (string stringKey in unencryptedStrings.Keys)
            {
                if (strings.Length > 0)
                {
                    strings.Append("&");
                }

                strings.Append(string.Format("{0}={1}", stringKey, unencryptedStrings[stringKey]));
            }

            return Encryption64.Encrypt(strings.ToString(), key);
        }

        /// <summary>
        /// Decrypt query string.
        /// </summary>
        /// <param name="encryptedStrings">Encrypted query string.</param>
        /// <param name="key">Key, being used to decrypt.</param>
        /// <remarks>The query string object replaces '+' character with an empty character.</remarks>
        /// <returns>Decrypt string.</returns>
        public static string DecryptQueryStrings(string encryptedStrings, string key)
        {
            return Encryption64.Decrypt(encryptedStrings.Replace(" ", "+"), key);
        }
    }
}
