using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using static Shared.Enums;

namespace Domain.Entities.Concrete
{
    public class User : EntityBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CellPhone { get; set; }
        public UserRoleEnum Role { get; set; }
        public string? Email { get; set; }
        public string? SignitureImgPath { get; set; }
        public string? AvatarImgPath { get; set; }
        public DateTime? BlockedAt { get; set; }


        [ForeignKey("User")]
        public int? CreatedById { get; set; }
        public virtual User UserCreatedBy { get; set; }
        public List<User> CreatedUsers { get; set; }

    }
}
