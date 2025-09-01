using StorageProject.Domain.Abstractions;
using StorageProject.Domain.Entity.Enums;

namespace StorageProject.Domain.Entity
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserProfile Profile{ get; set; }

        public User() { }
    }
}
