using STMS.Persistence.Models;
using STMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMS.Services.Services
{
    public interface IStudentService
    {
        Task<List<ReadStudentDto>> GetAllAsync(int pageNumber, int pageSize, string searchString, string sortBy);
        Task<ReadStudentDto?> GetByIdAsync(int id);
        Task<Student> InsertAsync(Student student);
        Task<Student?> UpdateAsync(int id, Student student);
        Task<Student?> DeleteAsync(int id);
    }
}
