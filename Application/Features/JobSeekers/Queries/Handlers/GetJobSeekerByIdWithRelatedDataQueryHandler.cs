﻿using MediatR;
using AutoMapper;
using Application.Contracts.Persistence;
using Domain.Entities.Concrete;
using Application.Features.JobSeekers.Dtos;
using Shared.Exceptions;

namespace Application.Features.JobSeekers.Queries.Handlers
{
    public class GetJobSeekerByIdWithRelatedDataQueryHandler : IRequestHandler<GetJobSeekerByIdWithRelatedDataQuery, JobSeekerDto>
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IMapper _mapper;

        public GetJobSeekerByIdWithRelatedDataQueryHandler(IJobSeekerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<JobSeekerDto> Handle(GetJobSeekerByIdWithRelatedDataQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _repository.GetByIdWithRelatedDataAsync(request.Id);
            if (jobSeeker == null)
            {
                throw new NotFoundException(nameof(JobSeeker), request.Id);
            }

            return _mapper.Map<JobSeekerDto>(jobSeeker);
        }
    }
}
