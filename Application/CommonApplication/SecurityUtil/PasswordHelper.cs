
using Konscious.Security.Cryptography;
using System.Text;

namespace Application.CommonApplication.SecurityUtil;


public class PasswordHasher
{
    public static string HashPassword(string password, byte[] salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 8, // تعداد Thread ها
            Iterations = 4,          // تعداد تکرار
            MemorySize = 1024 * 64   // حافظه مصرفی (64MB)
        };

        var hash = argon2.GetBytes(32); // خروجی 32 بایت
        return Convert.ToBase64String(hash);
    }
}
