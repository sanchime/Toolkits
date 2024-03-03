using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace Sanchime.Identity.Helpers;

internal static class PasswordHelper
{
    private static string HashPassword(string password, string salt)
    {
        var valueBytes = KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.UTF8.GetBytes(salt), //盐
            prf: KeyDerivationPrf.HMACSHA512, //伪随机函数，这里是SHA-512
            iterationCount: 10000, //迭代次数
            numBytesRequested: 256 / 8);//最后输出的秘钥长度

        return Convert.ToBase64String(valueBytes);
    }

    private static string GenerateSalt()
    {
        byte[] randomBytes = new byte[128 / 8];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    private static bool Validate(string password, string salt, string hash)
            => HashPassword(password, salt) == hash;

    public static string HashPassword(string password)
    {
        var salt = GenerateSalt();
        var hash = HashPassword(password, salt);
        return $"{salt}.{hash}";
    }


    public static bool VerifyHashedPassword(string password, string storePassword)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        if (string.IsNullOrEmpty(storePassword))
        {
            throw new ArgumentNullException(nameof(storePassword));
        }

        var parts = storePassword.Split('.');
        var salt = parts[0];
        var hash = parts[1];

        return Validate(password, salt, hash);
    }

}
