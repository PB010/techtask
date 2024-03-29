﻿using System;
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

        public static bool DecryptPassword(string dbPassword, string loginPassword)
        {
            var dbHashBytesPassword = Convert.FromBase64String(dbPassword);

            var salt = new byte[16];
            Array.Copy(dbHashBytesPassword, 0, salt, 0, 16);

            var inputHashBytesPassword = new Rfc2898DeriveBytes(loginPassword, salt, 10000);
            var hashFromInput = inputHashBytesPassword.GetBytes(20);

            for (var i = 0; i < 20; i++)
                if (dbHashBytesPassword[i + 16] != hashFromInput[i])
                    return false;

            return true;
        }
    }
}
