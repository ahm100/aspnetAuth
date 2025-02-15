using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain.Entities.Concrete;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /* public class UserRepository : RepositoryBase<User>, IUserRepository
     {
         protected readonly ApplicationDbContext _dbContext;

         public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
         {
             _dbContext = dbContext;
         }


         public async Task<int> CountByFilterAsync(UserFilter filter, CancellationToken cancellationToken)
         {
             var query = _dbContext.User.AsQueryable();

                 query = ApplyFilter(filter, query);

                 return await query.CountAsync(cancellationToken);
         }

         public async Task<User> GetByFilterAsync(UserFilter filter,
             CancellationToken cancellationToken)
         {
             var query = _dbContext.User.AsQueryable();

             query = ApplyFilter(filter, query);

             return await query.FirstOrDefaultAsync(cancellationToken);
         }

         public async Task<List<User>> GetListByFilterAsync(UserFilter filter,
             CancellationToken cancellationToken)
         {
             var query = _dbContext.User.AsQueryable();

             query = ApplyFilter(filter, query);

             query = ApplySorting(filter, query);

             if (filter.CurrentPage > 0)
                 query = query.Skip((filter.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize);

             return await query.ToListAsync(cancellationToken);
         }

         private static IQueryable<User> ApplySorting(UserFilter filter,
             IQueryable<User> query)
         {
             query = filter?.OrderBy.ToLower() switch
             {
                 "firstname" => filter.SortBy.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                     ? query.OrderBy(x => x.Firstname)
                     : query.OrderByDescending(x => x.Firstname),
                 "lastname" => filter.SortBy.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                     ? query.OrderBy(x => x.Lastname)
                     : query.OrderByDescending(x => x.Lastname),

                 _ => query
             };

             return query;
         }

         private static IQueryable<User> ApplyFilter(UserFilter filter,
             IQueryable<User> query)
         {
             if (filter.Id > 0)
                 query = query.Where(x => x.Id == filter.Id);

             if (!string.IsNullOrWhiteSpace(filter.Firstname))
                 query = query.Where(x => EF.Functions.Like(x.Firstname, $"%{filter.Firstname}%"));

             if (!string.IsNullOrWhiteSpace(filter.Lastname))
                 query = query.Where(x => EF.Functions.Like(x.Lastname, $"%{filter.Lastname}%"));

             if (!string.IsNullOrWhiteSpace(filter.Email))
                 query = query.Where(x => x.Email == filter.Email);

             if (!string.IsNullOrWhiteSpace(filter.Username))
                 query = query.Where(x => EF.Functions.Like(x.Username, $"%{filter.Username}%"));


             return query;
         }

     }*/
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            //dbContext.SaveChanges();
        }

        //public async Task<IReadOnlyList<User>> GetBySkillAsync(string skill, int pageNumber = 1, int pageSize = 10)
        //{
        //    return await GetAsync(
        //        js => js.Skills.Any(s => s..SkillName == skill),
        //        pageNumber: pageNumber,
        //        pageSize: pageSize);
        //}

        //public async Task<IReadOnlyList<User>> GetBySomeSkillsAsync(List<string> recskills, int pageNumber = 1, int pageSize = 10)
        //{
        //    return await GetAsync(
        //        js => js.Skills.Any(s => recskills.Contains(s.SkillName)),
        //        pageNumber: pageNumber,
        //        pageSize: pageSize
        //    );
        //}



        //public async Task<IReadOnlyList<User>> GetByBirthYearAsync(int year, int pageNumber = 1, int pageSize = 10)
        //{
        //    return await GetAsync(js => js.DateOfBirth.Year == year, pageNumber: pageNumber, pageSize: pageSize);
        //}

        public async Task<IReadOnlyList<User>> GetOrderedByLastNameAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await GetAsync(
                predicate: null,
                orderBy: query => query.OrderBy(js => js.LastName),
                includes: new string[] { },  //[posts, advertiser,...] could include any relaed table
                disableTracking: true, pageNumber: pageNumber, pageSize: pageSize);
        }


        //public async Task<IReadOnlyList<JobSeeker>> GetWithRelatedDataAsync(int pageNumber = 1, int pageSize = 10)
        //{
        //    var includes = new string[] { "Skills", "Experience" };
        //    return await GetAsync(
        //        predicate: null,
        //        orderBy: null,
        //        includes: includes,
        //        disableTracking: true,
        //        pageNumber: pageNumber,
        //        pageSize: pageSize
        //    );
        //}
        public async Task<User> GetByIdWithRelatedDataAsync(int id)
        {
            var includes = new string[] { "Skills", "Experience" };
            var users = await GetAsync(
                predicate: js => js.Id == id,
                includes: includes,
                disableTracking: true);
            return users.FirstOrDefault();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var users = await GetAsync(js => js.Email == email, orderBy: null,
                includes: new string[] { },  //[posts, advertiser,...] could include any relaed table
                disableTracking: true);
            return users.FirstOrDefault();
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {

            var user = await GetFirstOrDefaultAsync(usr => usr.UserName == userName);
            return user;
                
        }

        public async Task<int> AddRegisterAsync(User user)
        {
            User usr = await AddAsync(user);
            return usr.Id; 
            // if nout found the ExceptionHandlingMiddleware will handle it

        }

    }
}

