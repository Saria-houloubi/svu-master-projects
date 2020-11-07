using System;
using System.Security.Cryptography;

namespace AMW.Shared.Extensioins
{
    /// <summary>
    /// Extensions methods to encrpt strings
    /// </summary>
    public static class StringEncryptionExtensions
    {

        #region Properties
        /// <summary>
        /// The itreation on the hash function (100 --> 1000)
        /// </summary>
        public static int HashIteration { get; private set; } = 500;
        #endregion
        /// <summary>
        /// Hashes the password with a salt
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(this string password)
        {
            //Create the salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            //Create the Rfc2898DeriveBytes
            var rfc = new Rfc2898DeriveBytes(password, salt, HashIteration);
            //Get the hash password
            byte[] hash = rfc.GetBytes(20);
            //combain the password with its salt
            var hashPassword = new byte[36];
            Array.Copy(salt, 0, hashPassword, 0, 16);
            Array.Copy(hash, 0, hashPassword, 16, 20);
            //return the string password
            return Convert.ToBase64String(hashPassword);
        }
        /// <summary>
        /// Vertifies if the user provided the right passord
        ///     by hashing the  user password and comparing it to the password in the database
        /// </summary>
        /// <param name="dbPassword">The password that is saved in the database</param>
        /// <param name="userPassword">The user password that is provided one logging in</param>
        /// <returns></returns>
        public static bool VertifyPassword(this string dbPassword, string userPassword)
        {
            //Get the byte array 
            var dbBytePassword = Convert.FromBase64String(dbPassword);
            //Get the salt
            var salt = new byte[16];
            //Copy to the salt array
            Array.Copy(dbBytePassword, 0, salt, 0, 16);
            //Create the Rfc2898DeriveBytes
            var rfc = new Rfc2898DeriveBytes(userPassword, salt, HashIteration);
            //Get the hash password
            byte[] hash = rfc.GetBytes(20);
            //Compare values
            for (int i = 0; i < hash.Length; i++)
            {
                if (hash[i] != dbBytePassword[salt.Length + i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
