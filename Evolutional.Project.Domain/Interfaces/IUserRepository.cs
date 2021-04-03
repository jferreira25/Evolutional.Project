using Evolutional.Project.Domain.Entities;
using Evolutional.Project.Domain.Queries.Users.GetAllUsers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> GetByUserAsync(string name,string password);
        Task<IEnumerable<Users>> GetAllAsync(GetAllUsersQuery query);
        Task<long> AddAsync(Users user);
        Task<Users> GetByIdAsync(long id);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateAsync(Users user);
    }
}
