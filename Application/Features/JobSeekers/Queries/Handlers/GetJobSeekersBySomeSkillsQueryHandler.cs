﻿using MediatR;
using AutoMapper;
using Application.Contracts.Persistence;
using Domain.Entities.Concrete;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.JobSeekers.Dtos;

namespace Application.Features.JobSeekers.Queries.Handlers
{
    public class GetJobSeekersBySomeSkillsQueryHandler : IRequestHandler<GetJobSeekersBySomeSkillsQuery, List<JobSeekerDto>>
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IMapper _mapper;

        public GetJobSeekersBySomeSkillsQueryHandler(IJobSeekerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<JobSeekerDto>> Handle(GetJobSeekersBySomeSkillsQuery request, CancellationToken cancellationToken)
        {
            var jobSeekers = await _repository.GetBySomeSkillsAsync(request.Skills, request.PageNumber, request.PageSize);
            return _mapper.Map<List<JobSeekerDto>>(jobSeekers);
        }
    }
}
