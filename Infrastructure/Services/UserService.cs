using Application.Contracts.Persistence;
using Application.Contracts.ServiceInterfaces;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> RegisterUserAsync(RegisterModel model)
        {
            var existingUser = await _userRepository.GetByUserNameAsync(model.UserName);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            var user = new User
            {
                UserName = model.UserName,
                Role = model.Role
            };

            user.Password = _passwordHasher.HashPassword(user, model.Password);
            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<User> LoginUserAsync(LoginModel model)
        {
            var user = await _userRepository.GetByUserNameAsync(model.UserName);
            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid credentials");
            }

            return user;
        }
    }

}
