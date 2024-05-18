using System.Text;

namespace AuthenticationVeAuthorization.WebAPI.Utilities;

public class HashingHelper
{
    public static void CreatePassword(string password, out byte[] hash, out byte[] salt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        {
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != passwordHash[i]) return false;
            }

            return true;
        }
    }
}
