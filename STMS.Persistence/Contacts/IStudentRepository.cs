using STMS.Persistence.Models;

namespace STMS.Persistence.Contacts
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync(int pageNumber, int pageSize, string searchString, string sortBy);
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> InsertAsync(Student student);
        Task<Student?> UpdateAsync(int id, Student student);
        Task<Student?> DeleteAsync(int id);
    }
}
