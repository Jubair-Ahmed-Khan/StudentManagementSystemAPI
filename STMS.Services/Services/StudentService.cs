using STMS.Persistence.Contacts;
using STMS.Persistence.Models;
using STMS.Services.DTOs;
using STMS.Services.Mappers;


namespace STMS.Services.Services
{
    public class StudentService : IStudentService
    {
        public readonly IStudentRepository _repository;
        private readonly IStudentMappers _mapper;

        public StudentService(IStudentRepository repository, IStudentMappers mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //Get All Student Method 
        public async Task<List<ReadStudentDto>> GetAllAsync(int pageNumber, int pageSize, string searchString, string sortBy)
        {
            var allStudents = await _repository.GetAllAsync(pageNumber, pageSize, searchString, sortBy);
            var studentDTOs = await _mapper.MapStudentsToDto(allStudents); // Only map the items, not the entire PagedList
            return studentDTOs;
        }

        //Get Single Student By Id Method
        public async Task<ReadStudentDto?> GetByIdAsync(int id)
        {

            var selectedStudent = await _repository.GetByIdAsync(id);
            var studentDTos = await _mapper.MapStudentToReadDto(selectedStudent);
            return studentDTos;
        }

        //Add Single Student Method
        public async Task<Student> InsertAsync(Student student)
        {
            return await _repository.InsertAsync(student);
        }

        //Update Single Student By Id Method
        public async Task<Student?> UpdateAsync(int id, Student student)
        {
            return await _repository.UpdateAsync(id, student);
        }

        //Delete Single Student By Id Method
        public async Task<Student?> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
