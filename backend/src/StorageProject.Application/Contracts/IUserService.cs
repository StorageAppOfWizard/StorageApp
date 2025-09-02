using Ardalis.Result;
using StorageProject.Application.DTOs.User;

namespace StorageProject.Application.Contracts
{
    public interface IUserService
    {
        public Task<Result<List<UserDTO>>> GetAllAsync();
        public Task<Result<UserDTO>> GetByIdAsync(Guid id);
        public Task<Result> CreateAsync(CreateUserDTO createUserDTO);
        public Task<Result> UpdateAsync(UpdateUserDTO updateUserDTO);
        public Task<Result> RemoveAsync(Guid id);
    }
}
