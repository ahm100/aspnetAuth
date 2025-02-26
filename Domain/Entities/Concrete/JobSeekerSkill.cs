﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities.Concrete
{
    public class JobSeekerSkill : EntityBase
    {
        [ForeignKey("JobSeeker")] 
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
        public string SkillName { get; set; }
    }
}
