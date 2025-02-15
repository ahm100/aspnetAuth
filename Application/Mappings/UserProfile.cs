using AutoMapper;
using Application.Features.Users.Commands;
using Application.Features.Users.Dtos;
//using Application.Features.JobSeekers.Queries;
using Domain.Entities.Concrete;
using Application.Contracts.Persistence;

namespace Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<CreateUserCommand, User>()
            //    .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(skill => new JobSeekerSkill { SkillName = skill.SkillName }).ToList()))
            //    .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience.Select(exp => new JobSeekerExperience { Company = exp.Company, Role = exp.Role, StartJobDate = exp.StartJobDate, EndJobDate = exp.EndJobDate,Experience = exp.Experience }).ToList()));
            ////CreateMap<JobSeekerSkillDto, JobSeekerSkill>();
            //CreateMap<JobSeekerExperienceDto, JobSeekerExperience>();




            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForMember(dest => dest.UserName, opt => opt.Ignore());

            //CreateMap<User, UserDto>()
            //   .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(skill => new JobSeekerSkillDto { SkillName = skill.SkillName })))
            //    .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience.Select(exp => new JobSeekerExperienceDto { Company = exp.Company, Role = exp.Role, StartJobDate = exp.StartJobDate, EndJobDate = exp.EndJobDate,Experience = exp.Experience })));

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
            //CreateMap<Domain.Entities.User, UserResponseDto>()
            //    .ForMember(dest => dest.Username, opt => opt.Ignore())
            //    .AfterMap((source, destination) => { destination.Username = source.GetFullName(); });

            //CreateMap<UserFilterDto, UserFilter>();

            //CreateMap<Pagination<Domain.Entities.User>, PaginationDto<UserResponseDto>>()
            //    .AfterMap((source, converted, context) =>
            //    {
            //        converted.Result = context.Mapper.Map<List<UserResponseDto>>(source.Result);
            //    });

        }
    }
}
