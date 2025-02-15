using MediatR;
using Application.Features.Users.Dtos;
using Application.Features.Users.Dtos;

namespace Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
