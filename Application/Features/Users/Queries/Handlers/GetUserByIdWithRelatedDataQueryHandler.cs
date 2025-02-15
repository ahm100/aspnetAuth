using MediatR;
using AutoMapper;
using Application.Contracts.Persistence;
using Shared.Exceptions;
using Domain.Entities.Concrete;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries;

namespace Application.Features.Users.Queries.Handlers
{
    public class GetUserByIdWithRelatedDataQueryHandler : IRequestHandler<GetUserByIdWithRelatedDataQuery, UserDto>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUserByIdWithRelatedDataQueryHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdWithRelatedDataQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdWithRelatedDataAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
