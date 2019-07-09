using System;
using System.Security.Cryptography;

namespace TechTask.Infrastructure.Authentication
{
    public class PasswordHasher
    {
        public readonly string HashedPassword;

        public PasswordHasher(string password)
        {
            HashedPassword = GenerateHashedPassword(password);
        }

        private static string GenerateHashedPassword(string password)
        {
            var salt = GenerateSalt();
            var hashedPass = ComputeHash(password, salt);
            
            return Convert.ToBase64String(hashedPass);
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            return salt;
        }

        private static byte[] ComputeHash(string password, byte[] salt)
        {
            var hashGenerator = new Rfc2898DeriveBytes(password, salt, 10000);
            var hash = hashGenerator.GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return hashBytes;
        }
    }
}
