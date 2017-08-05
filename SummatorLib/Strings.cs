using System;
using System.Security.Cryptography;
using System.Text;

namespace SummatorLib
{
    public static class Strings
    {

        /// <summary>
        /// Calculates md5 sum of string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Md5Sum(string text)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            return CustomSum(provider, text);
        }

        /// <summary>
        /// Calculates sha1 sum of string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Sha1Sum(string text)
        {
            SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();
            return CustomSum(provider, text);
        }

        /// <summary>
        /// Calculates sha256 sum of string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Sha256Sum(string text)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
            return CustomSum(provider, text);
        }

        /// <summary>
        /// Calculates sha512 sum of string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Sha512Sum(string text)
        {
            SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider();
            return CustomSum(provider, text);
        }

        /// <summary>
        /// Calculates custom sum
        /// </summary>
        /// <param name="algorithm">Hash algorithm</param>
        /// <param name="text">Calculating text</param>
        /// <returns></returns>
        private static string CustomSum(HashAlgorithm algorithm, string text)
        {
            byte[] enc = algorithm.ComputeHash(Encoding.ASCII.GetBytes(text));
            return Utils.ByteaToString(enc);
        }

        /// <summary>
        /// Calculates Base64 string
        /// </summary>
        /// <returns></returns>
        public static string ToBase64(this string text)
        {
            // https://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
            var bytea = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytea);
        }

    }
}
