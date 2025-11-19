namespace StorageProject.Application.Contracts
{
    public interface IUserContextAuth
    {
        string UserId {  get; }
        bool IsAuthenticated { get; }
    }
}
