using MediatR;
using System;
using System.Collections.Generic;
using Application.Features.Users.Dtos;
using static Shared.Enums;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Cellphone { get; set; }
        public UserRoleEnum Role { get; set; }
        public string Email { get; set; }
        public string SignitureImgPath { get; set; }
        public string AvatarImgPath { get; set; }
        public DateTime BlockedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int CreatedBy { get; set; }
    }
}
