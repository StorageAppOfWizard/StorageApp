using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.User;
using StorageProject.Application.Extensions;
using StorageProject.Application.Mappers;
using StorageProject.Application.Validators;
using StorageProject.Domain.Contracts;

namespace StorageProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<UserDTO>>> GetAllAsync()
        {
            var entity = await _unitOfWork.UserRepository.GetAll();
            if (entity is null)
                return Result.NotFound("No users found");

            return Result.Success(entity.Select(u => u.ToDTO()).ToList());
        }

        public async Task<Result<UserDTO>> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result.Error("Invalid user ID provided.");

            var entity = await _unitOfWork.UserRepository.GetById(id);

            if (entity is null)
                return Result.NotFound("User not found");

            return Result.Success(entity.ToDTO());

        }
        public async Task<Result> CreateAsync(CreateUserDTO dto)
        {
            var validation = dto.ToValidateErrors(new UserValidator());
            if (validation.Count != 0)
                return Result.Invalid(validation);

            var existingUser = await _unitOfWork.UserRepository.GetByNameAsync(dto.Name);
            if (existingUser != null)
                return Result.Conflict($"User with the name '{existingUser.Name}' already exists.");

            var entity = dto.ToEntity();

            await _unitOfWork.UserRepository.Create(entity);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("User Created");
        }

        public async Task<Result> UpdateAsync(UpdateUserDTO dto)
        {
            var validation = dto.ToValidateErrors(new UserValidator());
            if (validation.Count != 0)
                return Result.Invalid(validation);

            var existingUser = await _unitOfWork.UserRepository.GetByNameAsync(dto.Name);
            if (existingUser != null)
                return Result.Conflict($"User with the name '{existingUser.Name}' already exists.");

            var entity = await _unitOfWork.UserRepository.GetById(dto.Id);
            if (entity is null)
                return Result.NotFound("User Not Found");

            dto.ToEntity(entity);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("User Updated");
        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result.Error("Invalid User ID provided.");

            var entity = await _unitOfWork.UserRepository.GetById(id);

            if (entity == null)
                return Result.NotFound("User not found");

            _unitOfWork.UserRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return Result.SuccessWithMessage("User deleted successfully.");
        }

    }
}
