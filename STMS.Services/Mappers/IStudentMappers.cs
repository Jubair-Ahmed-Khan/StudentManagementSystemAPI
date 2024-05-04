using STMS.Persistence.Models;
using STMS.Services.DTOs;

namespace STMS.Services.Mappers
{
    public interface IStudentMappers
    {
        Task<List<ReadStudentDto>> MapStudentsToDto(List<Student> students);
        Student MapCreateDtoToStudent(CreateStudentDto dto);
        Task<List<ReadStudentsDto>> MapStudentsToReadDto(Task<List<Student>> studentsTask);
        Task<ReadStudentDto> MapStudentToReadDto(Student student);
        Student MapUpdateDtoToStudent(UpdateStudentDto dto);
    }
}
