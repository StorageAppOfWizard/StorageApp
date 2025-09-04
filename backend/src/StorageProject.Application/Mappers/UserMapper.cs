using StorageProject.Application.DTOs.User;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Profile = user.Profile
            };
        }

        public static User ToEntity(this CreateUserDTO createUserDTO)
        {
            return new User
            {
                Name = createUserDTO.Name,
                Email = createUserDTO.Email,
                Password = createUserDTO.Password,
            };
        }

        public static void ToEntity(this UpdateUserDTO dto, User user)
        {
            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Password = dto.Password;
        }


    }
}
