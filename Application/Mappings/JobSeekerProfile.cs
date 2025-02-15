using AutoMapper;
using Application.Features.JobSeekers.Commands;
using Application.Features.JobSeekers.Dtos;
//using Application.Features.JobSeekers.Queries;
using Domain.Entities.Concrete;

namespace Application.Mapping
{
    public class JobSeekerProfile : Profile
    {
        public JobSeekerProfile()
        {
            CreateMap<CreateJobSeekerCommand, JobSeeker>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(skill => new JobSeekerSkill { SkillName = skill.SkillName }).ToList()))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience.Select(exp => new JobSeekerExperience { Company = exp.Company, Role = exp.Role, StartJobDate = exp.StartJobDate, EndJobDate = exp.EndJobDate,Experience = exp.Experience }).ToList()));
            CreateMap<JobSeekerSkillDto, JobSeekerSkill>();
            CreateMap<JobSeekerExperienceDto, JobSeekerExperience>();




            CreateMap<UpdateJobSeekerCommand, JobSeeker>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(skill => new JobSeekerSkill { SkillName = skill.SkillName }).ToList()))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience.Select(exp => new JobSeekerExperience { Company = exp.Company, Role = exp.Role, StartJobDate = exp.StartJobDate, EndJobDate = exp.EndJobDate, Experience = exp.Experience }).ToList()));

            CreateMap<JobSeeker, JobSeekerDto>()
               .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(skill => new JobSeekerSkillDto { SkillName = skill.SkillName })))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience.Select(exp => new JobSeekerExperienceDto { Company = exp.Company, Role = exp.Role, StartJobDate = exp.StartJobDate, EndJobDate = exp.EndJobDate,Experience = exp.Experience })));

        }
    }
}
