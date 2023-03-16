using System.Security.Cryptography;

namespace rock_paper_scissors_anything
{
    internal static class KeyGenerator
    {
        /// <summary>
        /// Creates a cryptographically secure random key string.
        /// </summary>
        /// <returns>A secure random string</returns>
        public static string CreateSecureRandomString()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(256));
        }
    }
}
