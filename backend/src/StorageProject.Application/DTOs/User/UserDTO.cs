using StorageProject.Domain.Entity.Enums;

namespace StorageProject.Application.DTOs.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserProfile Profile { get; set; }

    }
}
