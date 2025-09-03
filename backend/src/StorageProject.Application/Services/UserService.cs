//using Ardalis.Result;
//using StorageProject.Application.Contracts;
//using StorageProject.Application.DTOs.User;
//using StorageProject.Application.Extensions;
//using StorageProject.Application.Mappers;
//using StorageProject.Application.Validators;
//using StorageProject.Domain.Contracts;

//namespace StorageProject.Application.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public UserService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<Result<List<UserDTO>>> GetAllAsync()
//        {
//            var entity = await _unitOfWork.UserRepository.GetAll();
//            if (entity is null)
//                return Result.NotFound("No users found");
            
//            return Result.Success(entity.Select(u => u.ToDTO()).ToList());
//        }

//        public async Task<Result<UserDTO>> GetByIdAsync(Guid id)
//        {
//            if (id == Guid.Empty)
//                return Result.Error("Invalid user ID provided.");

//            var entity = await _unitOfWork.UserRepository.GetById(id);

//            if (entity is null)
//                return Result.NotFound("User not found");

//            return Result.Success(entity.ToDTO());

//        }
//        //public Task<Result> CreateAsync(CreateUserDTO createUserDTO)
//        //{

//        //    //To do create user service
//        //    var validation = createUserDTO.ToValidateErrors(new UserValidator());
//        //    if (validation.Count != 0)
//        //        return Result.Invalid(validation);

//        //    var existingBrand = await _unitOfWork.BrandRepository.GetByNameAsync(createUserDTO.Name);
//        //    if (existingBrand != null)
//        //        return Result.Conflict($"Brand with the name '{existingBrand.Name}' already exists.");

//        //    var entity = createUserDTO.ToEntity();

//        //    var brand = await _unitOfWork.BrandRepository.Create(entity);
//        //    await _unitOfWork.CommitAsync();

//        //    return Result.SuccessWithMessage("Brand Created");
//        //}

//        public Task<Result> UpdateAsync(UpdateUserDTO updateUserDTO)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Result> RemoveAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//    }
//}
