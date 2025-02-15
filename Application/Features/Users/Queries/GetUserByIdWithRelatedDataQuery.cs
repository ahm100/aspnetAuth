using MediatR;
using Application.Features.Users.Dtos;

namespace Application.Features.Users.Queries
{
    public class GetUserByIdWithRelatedDataQuery : IRequest<UserDto>
    {
        public int Id { get; set; }

        public GetUserByIdWithRelatedDataQuery(int id)
        {
            Id = id;
        }
    }
}
