using System;
using System.Collections.Generic;
using Konscious.Security.Cryptography;
using System.Text;
using System.Security.Cryptography;

namespace Domain.Common.Domain.ValueObjects;

public class Password: BaseValueObject
{
    public string Hash { get; private set; }
    public string Salt { get; private set; }

    private Password(string hash, string salt)
    {
        Hash = hash;
        Salt = salt;
    }

    public static Password Create(string plainPassword)
    {
        var saltBytes = GenerateSalt();
        var hash = HashPassword(plainPassword, saltBytes);
        return new Password(hash, Convert.ToBase64String(saltBytes));
    }

    public bool Verify(string plainPassword)
    {
        var saltBytes = Convert.FromBase64String(Salt);
        var hashToCompare = HashPassword(plainPassword, saltBytes);
        return Hash == hashToCompare;
    }

    private static string HashPassword(string password, byte[] salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 8,
            Iterations = 4,
            MemorySize = 1024 * 64
        };

        var hash = argon2.GetBytes(32);
        return Convert.ToBase64String(hash);
    }

    private static byte[] GenerateSalt(int size = 16)
    {
        var salt = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }
}

