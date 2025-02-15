using MediatR;
using FluentValidation;
using Application.Contracts.Persistence;
using Domain.Entities.Concrete;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserRepository _repository;
        private readonly IValidator<User> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserHandler> _logger;

        public UpdateUserHandler(IUserRepository repository, IValidator<User> validator, IMapper mapper, ILogger<UpdateUserHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            _mapper.Map(request, user);

            var validationResult = _validator.Validate(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _repository.UpdateAsync(user);
            _logger.LogInformation($"User {user.Id} updated successfully.");
            return user.Id;
        }
    }
}
