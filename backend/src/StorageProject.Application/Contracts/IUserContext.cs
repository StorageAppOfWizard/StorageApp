namespace StorageProject.Application.Contracts
{
    public interface IUserContext
    {
        string UserId {  get; }
        bool IsAuthenticated { get; }
    }
}
