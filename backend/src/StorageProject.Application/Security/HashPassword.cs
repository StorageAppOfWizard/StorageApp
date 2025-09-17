using BCrypt.Net;

namespace StorageProject.Application.Security
{
    public class HashPassword : IHashPassword
    {
        public string GenerateHasPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password,hash);
        }
    }
}
