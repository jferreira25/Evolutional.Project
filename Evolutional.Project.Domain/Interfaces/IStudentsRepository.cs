using Evolutional.Project.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Interfaces
{
    public interface IStudentsRepository
    {
        Task<IEnumerable<Students>> GetAllAsync(string name, int offSet, int pageLength);
        Task<IEnumerable<Students>> GetAllAsync();
        Task<Students> GetByNameAsync(string name);
        Task<long> AddAsync(Students student);
        Task<Students> GetByIdAsync(long id);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateAsync(Students students);
    }
}
