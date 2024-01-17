using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Services
{
    public static class PasswordHashing
    {
        public static byte[] HashPassword(string rawpassword, byte[] salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(rawpassword + salt);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                return hashedBytes;
            }
        }
        public static byte[] GenerateTimestampSalt()
        {
            long ticks = DateTime.UtcNow.Ticks;
            byte[] saltBytes = BitConverter.GetBytes(ticks);
            return saltBytes;
        }

    }
}