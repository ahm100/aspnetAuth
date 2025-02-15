using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities.Concrete
{

    public class Branch : BasicActioner
    {
        public int Code { get; set; }
        public string PrimaryTitle { get; set; }
        public string? SeocondaryTitle { get; set; }
        public bool IsActive { get; set; }
        
        //[ForeignKey("User")]
        //public int? CreatedBy { get; set; }
        //public virtual User UserCreatedBy { get; set; }

        //[ForeignKey("User")]
        //public int? UpdatedBy { get; set; }
        //public virtual User UserUpdatedBy { get; set; }

        //[ForeignKey("User")]
        //public int? DeletedBy { get; set; }
        //public virtual User UserDeletedBy { get; set; }


    }
}