using MediatR;
using FluentValidation;
using Application.Contracts.Persistence;
using Domain.Entities.Concrete;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using Application.Resources;

namespace Application.Features.Users.Commands.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _repository;
        private readonly IValidator<CreateUserCommand> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly IStringLocalizer<JobSeekerResource> _localizer;

        public CreateUserHandler(IUserRepository repository, IValidator<CreateUserCommand> validator, IMapper mapper, ILogger<CreateUserHandler> logger, IStringLocalizer<JobSeekerResource> localizer)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Check if the email already exists
            var existingUser = await _repository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new ValidationException(_localizer["EmailAlreadyExists"]);
            }

            var user = _mapper.Map<User>(request);

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _repository.AddAsync(user);
           // _logger.LogInformation(_localizer["ExperienceAdded"]);
            return result.Id;
        }

    }
}
