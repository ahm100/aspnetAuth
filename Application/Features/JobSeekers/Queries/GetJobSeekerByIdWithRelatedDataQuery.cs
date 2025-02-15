using MediatR;
using Application.Features.JobSeekers.Dtos;

namespace Application.Features.JobSeekers.Queries
{
    public class GetJobSeekerByIdWithRelatedDataQuery : IRequest<JobSeekerDto>
    {
        public int Id { get; set; }

        public GetJobSeekerByIdWithRelatedDataQuery(int id)
        {
            Id = id;
        }
    }
}
