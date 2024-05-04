namespace STMS.Services.DTOs
{
    public class CreateStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}

