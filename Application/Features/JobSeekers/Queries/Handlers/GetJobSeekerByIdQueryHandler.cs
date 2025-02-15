using MediatR;
using AutoMapper;
using Application.Contracts.Persistence;
using Domain.Entities.Concrete;
using Application.Features.JobSeekers.Dtos;
using Shared.Exceptions;

namespace Application.Features.JobSeekers.Queries.Handlers
{
    public class GetJobSeekerByIdQueryHandler : IRequestHandler<GetJobSeekerByIdQuery, JobSeekerDto>
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IMapper _mapper;

        public GetJobSeekerByIdQueryHandler(IJobSeekerRepository repository, 
            IMapper mapper)           
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<JobSeekerDto> Handle(GetJobSeekerByIdQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _repository.GetByIdAsync(request.Id);
            if (jobSeeker == null)
            {
                throw new NotFoundException(nameof(JobSeeker), request.Id);
                //throw new NotFoundException(_localizer["Hello World"]);
            }

            return _mapper.Map<JobSeekerDto>(jobSeeker);
        }
    }
}
