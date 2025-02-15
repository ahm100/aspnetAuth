using MediatR;

namespace Application.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }
    }
}
