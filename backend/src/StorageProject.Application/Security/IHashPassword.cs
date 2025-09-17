namespace StorageProject.Application.Security
{
    public interface IHashPassword
    {
        string GenerateHasPassword(string password);
        bool VerifyPassword(string password, string hash);

    }
}
