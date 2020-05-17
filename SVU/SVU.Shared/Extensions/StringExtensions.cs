using System;
using System.Security.Cryptography;
using System.Text;

namespace SVU.Shared.Extensions
{
    /// <summary>
    /// Some string extension methods
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Hashes the value using MD5 algorithm
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string GetMD5Hash(this string plainText)
        {
            //Check if the data was provided
            if (!string.IsNullOrEmpty(plainText))
            {
                using (var md5 = MD5.Create())
                {
                    //Get the byte data
                    var data = md5.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                    //The string that will hold the hash
                    var strBuilder = new StringBuilder();

                    //Loop throw the data and get a hexa string
                    for (int i = 0; i < data.Length; i++)
                    {
                        strBuilder.Append(data[i].ToString("x2"));
                    }
                    //Build and return the string
                    return strBuilder.ToString();
                }

            }
            return null;
        }
        /// <summary>
        /// Vertifies if the input matches the md5 hash
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyMD5Hash(this string input, string hash)
        {
            //Get the md5 hash value
            var hashInput = input.GetMD5Hash();

            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

            if (stringComparer.Compare(input, hash) == 0)
            {
                return true;
            }

            return false;
        }
    }

}
