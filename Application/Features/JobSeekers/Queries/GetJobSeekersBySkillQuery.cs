using MediatR;
using System.Collections.Generic;
using Application.Features.JobSeekers.Dtos;

namespace Application.Features.JobSeekers.Queries
{
    public class GetJobSeekersBySkillQuery : IRequest<List<JobSeekerDto>>
    {
        public string Skill { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetJobSeekersBySkillQuery(string skill, int pageNumber = 1, int pageSize = 10)
        {
            Skill = skill;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
