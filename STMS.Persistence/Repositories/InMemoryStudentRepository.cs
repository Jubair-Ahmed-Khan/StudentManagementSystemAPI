using Microsoft.EntityFrameworkCore;
using STMS.Persistence.Contacts;
using STMS.Persistence.Data;
using STMS.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMS.Persistence.Repositories
{
    public class InMemoryStudentRepository : IStudentRepository
    {
        private readonly InMemoryDBContext _dbContext;

        public InMemoryStudentRepository(InMemoryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get All Students Method
        public async Task<List<Student>> GetAllAsync(int pageNumber, int pageSize, string searchString, string sortBy)
        {
            try
            {
                var query = _dbContext.Students.AsQueryable();

                if (!string.IsNullOrEmpty(searchString))
                    query = query.Where(s => s.Name.Contains(searchString));

                // Sorting
                query = sortBy switch
                {
                    "Id" => query.OrderBy(s => s.Id),
                    "email" => query.OrderBy(s => s.Email),
                    _ => query.OrderByDescending(s => s.Name),
                };

                // Paging
                var totalItems = await query.CountAsync();
                var students = await query.Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToListAsync();
                return students;
            }
            catch (Exception ex)
            {
                string error = $"Error retrieving students: {ex.Message}";
                throw new Exception(error);
            }
        }


        //Get Single Student By Id Method
        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _dbContext.Students.FindAsync(id);
        }


        //Add Student Method
        public async Task<Student?> InsertAsync(Student student)
        {
            try
            {
                _dbContext.Students.Add(student);
                await _dbContext.SaveChangesAsync();
                return student;
            }
            catch (Exception ex)
            {
                string error = $"Error adding students: {ex.Message}";
                throw new Exception(error);
            }
        }

        //Update student method
        public async Task<Student?> UpdateAsync(int id, Student student)
        {
            var studentToUpdate = await _dbContext.Students.FirstOrDefaultAsync(y => y.Id == id);

            if (studentToUpdate == null)
                return null;

            studentToUpdate.Name = student.Name;
            studentToUpdate.Email = student.Email;
            studentToUpdate.EnrollmentDate = student.EnrollmentDate;

            await _dbContext.SaveChangesAsync();

            return studentToUpdate;
        }

        //Delete Student Method
        public async Task<Student?> DeleteAsync(int id)
        {
            var studentToDelete = await _dbContext.Students.FirstOrDefaultAsync(y => y.Id == id);

            if (studentToDelete == null)
                return null;

            _dbContext.Students.Remove(studentToDelete);
            await _dbContext.SaveChangesAsync();

            return studentToDelete;
        }
    }
}
