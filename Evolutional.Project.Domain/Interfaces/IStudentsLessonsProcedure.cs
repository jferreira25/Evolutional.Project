using Evolutional.Project.Domain.Dto;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Interfaces
{
    public interface IStudentsLessonsProcedure
    {
        Task<long> AddAsync(StudentsLessonsDto studentsLessons);
    }
}
