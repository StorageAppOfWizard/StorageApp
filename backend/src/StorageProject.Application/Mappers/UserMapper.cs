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

        public static User ToEntity(this UserDTO userDTO)
        {
            return new User
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                Email = userDTO.Email,
                Profile = userDTO.Profile,
            };
        }


    }
}
