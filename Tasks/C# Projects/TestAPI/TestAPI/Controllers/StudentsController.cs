using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Data;
using TestAPI.Model;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private ApplicationDbContext _db;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ApplicationDbContext context, ILogger<StudentsController> logger)
        {
            _db = context;
            _logger = logger;
        }

        [HttpGet("GetAllStudents")]
        public List<StudentEntity> GetAllStudents()
        {
            _logger.LogInformation("Fetching All Student List");
            return _db.StudentRegister.ToList();   
        }

        /*[HttpGet("GetStudentsName")]
        public string GetAllStudentsName()
        {
            return "Hello Students";
        }*/

        [HttpGet("GetStudentsById")]
        public ActionResult<StudentEntity> GetStudentDetails(int Id)
        {
            if (Id == 0)
            {
                _logger.LogError("Student Id was not passed");
                return BadRequest();
            }
            var StudentDetails = _db.StudentRegister.FirstOrDefault(x => x.Id == Id);

            if (StudentDetails == null) { return NotFound(); }
            return StudentDetails;   
        }

        [HttpPost]
        public ActionResult<StudentEntity> AddStudent([FromBody] StudentEntity StudentDetails)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            _db.StudentRegister.Add(StudentDetails);
            _db.SaveChanges();
            return Ok(StudentDetails);
        }

        [HttpPost("UpdateStudentDetails")]
        public ActionResult<StudentEntity> UpdateStudent(Int32 Id, [FromBody] StudentEntity StudentDetails)
        {
            if (StudentDetails==null)
            {
                return BadRequest(StudentDetails);
            }

            var studentDetails = _db.StudentRegister.FirstOrDefault(x => x.Id == Id);
            if (StudentDetails == null) { return NotFound(); }


            studentDetails.Name = StudentDetails.Name;
            studentDetails.Age = StudentDetails.Age;
            studentDetails.Standard = StudentDetails.Standard;
            studentDetails.EmailAddress = StudentDetails.EmailAddress;

            _db.SaveChanges();
            return Ok(StudentDetails);
        }

        [HttpPut("DeleteStudent")]
        public ActionResult<StudentEntity> DeleteStudent(int Id)
        {
            var StudentDetails = _db.StudentRegister.FirstOrDefault(x => x.Id == Id);
            if (StudentDetails == null)
            {
                return BadRequest(StudentDetails);
            }

            
            /*if (StudentDetails == null) { return NotFound(); }*/
            _db.Remove(StudentDetails);

            _db.SaveChanges();
            return NoContent();
        }

    }
}
