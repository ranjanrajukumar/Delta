using System.Collections.Generic;
using System.Threading.Tasks;
using Delta.Domain.Entities.Student;

namespace Delta.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> AddAsync(Student student);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int studentId);
        Task UpdateAsync(Student student);
        Task SoftDeleteAsync(int studentId);
    }
}
