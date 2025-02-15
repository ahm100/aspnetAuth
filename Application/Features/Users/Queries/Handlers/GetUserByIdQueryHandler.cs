using MediatR;
using AutoMapper;
using Application.Contracts.Persistence;
using Domain.Entities.Concrete;
using Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using Microsoft.Extensions.Localization;
using Application.Contracts;

namespace Application.Features.Users.Queries.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository repository, 
            IMapper mapper)           
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
                //throw new NotFoundException(_localizer["Hello World"]);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
