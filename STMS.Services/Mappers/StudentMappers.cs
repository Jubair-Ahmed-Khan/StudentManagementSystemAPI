using STMS.Persistence.Models;
using STMS.Services.DTOs;


namespace STMS.Services.Mappers
{
    public class StudentMappers:IStudentMappers
    {

        //Convert Students to List of ReadStudent Dto
        public async Task<List<ReadStudentDto>> MapStudentsToDto(List<Student> students)
        {
            List<ReadStudentDto> studentDTOs = new List<ReadStudentDto>();

            // Loop through each student and map it to StudentDTO
            foreach (var student in students)
            {
                studentDTOs.Add(new ReadStudentDto
                {
                    Id = student.Id,
                    Name = student.Name, // Ensure that these properties are correctly mapped
                    Email = student.Email,
                    EnrollmentDate = student.EnrollmentDate,
                });
            }

            
            return studentDTOs;
        }


        //Convert CreateStudentDto to Student Object
        public Student MapCreateDtoToStudent(CreateStudentDto dto)
        {
            return new Student
            {
                Name = dto.Name,
                Email = dto.Email,
                EnrollmentDate = DateTime.UtcNow
            };
        }


        //Convert Student Object to ReadStudentDto
        public async Task<ReadStudentDto> MapStudentToReadDto(Student student)
        {
            if(student!=null)
            {
                return new ReadStudentDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    EnrollmentDate = student.EnrollmentDate
                };
            }

            return null;
        }


        //Convert List of Student Object to List of ReadStudentDto
        public async Task<List<ReadStudentsDto>> MapStudentsToReadDto(Task<List<Student>> studentsTask)
        {
            var students = await studentsTask;
            List<ReadStudentsDto> studentDTOs = new List<ReadStudentsDto>();

            // Loop through each student and map it to ReadStudentDTO
            foreach (var student in students)
            {
                studentDTOs.Add(new ReadStudentsDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    EnrollmentDate = student.EnrollmentDate
                });
            }

            return studentDTOs;
        }
           

        //Convert UpdateStudentDto to Student Object
        public Student MapUpdateDtoToStudent(UpdateStudentDto dto)
        {
            return new Student
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email
            };
        }
    }
}
