using static Shared.Enums;

namespace Application.Contracts.Persistence
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRoleEnum Role { get; set; }
    }

}