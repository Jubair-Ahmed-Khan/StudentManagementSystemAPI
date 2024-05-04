using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using STMS.Persistence.Models;
using STMS.Services.Services;

namespace STMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("NextApp")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService service, ILogger<StudentController> logger)
        {
            _service = service;
            _logger = logger;
        }
       
        [HttpGet]
        [Route("GetAllStudents")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10, string searchString = "", string sortBy = "id")
        {
            _logger.LogInformation("Loading All Students");

            var students = await _service.GetAllAsync(pageNumber, pageSize, searchString, sortBy);

            if (students == null)
            {
                return NotFound("No Student found in the database");
            }

            _logger.LogInformation("Students loaded sucesfully");

            return Ok(students);
        }

        [HttpGet]
        [Route("GetStudentByID")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Finding a student by Id");

            var selectedSTudent = await _service.GetByIdAsync(id);

            if (selectedSTudent == null)
            {
                return NotFound("No student found by given id");
            }

            _logger.LogInformation("Successfully Found student by id");

            return Ok(selectedSTudent);
        }

        [HttpPost]
        [Route("AddStudent")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            try
            {
                _logger.LogInformation("Adding a student");

                var studentToInsert = await _service.InsertAsync(student);

                _logger.LogInformation("Student Added Successfully");

                return Ok(studentToInsert);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Internal server error: {ex.Message}";

                return StatusCode(500, errorMessage);
            }
        }


        [HttpPut]
        [Route("UpdateStudent")]
        [Authorize]
        public async Task<IActionResult> Update(int id, Student student)
        {
            try
            {
                _logger.LogInformation("Updating a student");

                var studentToUpdate = await _service.UpdateAsync(id, student);

                if (studentToUpdate != null)
                {
                    _logger.LogInformation("Student updated successfully");

                    return Ok(studentToUpdate); 
                }
                else
                {
                    return NotFound("No student found by given id");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Return 500 Internal Server Error
            }
        }


        [HttpDelete]
        [Route("DeleteStudent")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting a student");
                var studentToDelete = await _service.DeleteAsync(id);
                if (studentToDelete != null)
                {
                    _logger.LogInformation("Student Deleted Succesfully");
                    return Ok(studentToDelete);
                }
                else
                {
                    return NotFound("No student found by given id");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
