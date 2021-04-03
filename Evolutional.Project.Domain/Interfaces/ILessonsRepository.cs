using Evolutional.Project.Domain.Entities;
using Evolutional.Project.Domain.Queries.Lessons.GetFilterAllLessons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Interfaces
{
    public interface ILessonsRepository
    {
        Task<IEnumerable<Lesson>> GetAllAsync(GetFilterAllLessonsQuery getAllLessonsQuery);
        Task<IEnumerable<Lesson>> GetAllAsync();
        Task<Lesson> GetByNameAsync(string name);
        Task<long> AddAsync(Lesson lesson);
        Task<Lesson> GetByIdAsync(long id);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateAsync(Lesson lesson);
    }
}
