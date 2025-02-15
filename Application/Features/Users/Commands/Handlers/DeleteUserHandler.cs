using MediatR;
using Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Concrete;

namespace Application.Features.Users.Commands.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<DeleteUserHandler> _logger;

        public DeleteUserHandler(IUserRepository repository, ILogger<DeleteUserHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            await _repository.DeleteAsync(user);
            _logger.LogInformation($"User {user.Id} deleted successfully.");

            return Unit.Value;
        }
    }
}
