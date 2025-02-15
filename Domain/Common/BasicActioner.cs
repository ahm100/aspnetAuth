using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Concrete;

namespace Domain.Common
{
    public abstract class BasicActioner
    {
        [Key] 
        public int Id { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


        [ForeignKey("User")]
        public int? CreatedBy { get; set; }
        public virtual User UserCreatedBy { get; set; }

        [ForeignKey("User")]
        public int? UpdatedBy { get; set; }
        public virtual User UserUpdatedBy { get; set; }

        [ForeignKey("User")]
        public int? DeletedBy { get; set; }
        public virtual User UserDeletedBy { get; set; }


    }
}
