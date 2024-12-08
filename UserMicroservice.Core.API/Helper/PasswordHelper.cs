using System.Security.Cryptography;

namespace UserMicroservice.Core.API.Helper
{
    public class PasswordHelper
    {
        private const int SaltSize = 16; // 128-bit salt
        private const int KeySize = 32; // 256-bit key
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            // Generate a unique salt
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            // Hash the password with the salt
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(KeySize);

            // Combine the salt and hash
            var hashBytes = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

            // Convert to Base64 for storage
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            // Convert the stored hash from Base64
            var hashBytes = Convert.FromBase64String(storedHash);

            // Extract the salt from the stored hash
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Extract the actual hash
            var storedPasswordHash = new byte[KeySize];
            Array.Copy(hashBytes, SaltSize, storedPasswordHash, 0, KeySize);

            // Hash the input password with the extracted salt
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var inputPasswordHash = pbkdf2.GetBytes(KeySize);

            // Compare the computed hash with the stored hash
            return CryptographicOperations.FixedTimeEquals(inputPasswordHash, storedPasswordHash);
        }

    }
}
