using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Common
{
    public abstract class EntityBase
    {
        [Key] 
        public int Id { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        //[Required] 
        //public DateTime CreateDate { get; set; } = DateTime.Now; 
        //public DateTime? UpdateDate { get; set; }

        //[Timestamp] 
        //public byte[] RowVersion { get; set; }
    }
}
