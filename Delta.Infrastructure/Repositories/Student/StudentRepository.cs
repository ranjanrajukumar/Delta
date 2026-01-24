using Delta.Application.Interfaces;
using Delta.Infrastructure.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Delta.Domain.Entities.Student;

using System.Collections.Generic;
using System.Text;

namespace Delta.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Student> AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Where(x => x.DelStatus == 0)   // Active students only
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int studentId)
        {
            return await _context.Students
                .FirstOrDefaultAsync(x => x.StudentID == studentId && x.DelStatus == 0);
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null) return;

            student.DelStatus = 1; // Soft delete
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

    }
}
