using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain.Entities.Concrete;

namespace Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        //Task<IReadOnlyList<User>> GetBySkillAsync(string skill, int pageNumber = 1, int pageSize = 10);
        //Task<IReadOnlyList<User>> GetByBirthYearAsync(int year, int pageNumber = 1, int pageSize = 10);
        Task<IReadOnlyList<User>> GetOrderedByLastNameAsync(int pageNumber = 1, int pageSize = 10);
        
        Task<User> GetByIdWithRelatedDataAsync(int id);

        Task<User> GetByEmailAsync(string email);
        //Task<IReadOnlyList<User>> GetBySomeSkillsAsync(List<string> recskills, int pageNumber = 1, int pageSize = 10);


        Task<User> GetByUserNameAsync(string userName);

        Task<int> AddRegisterAsync(User user);
    }

    
}


